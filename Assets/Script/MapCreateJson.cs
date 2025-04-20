#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreateJson : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] Camera _camera;
    [Header("Grid Settings")]
    public string fileName = "gridData";
    public int rows = 10;
    public int cols = 10;
    public float cellSize = 1f;

    //draw 
    bool isDraw;
    bool isEmptyDraw;
    //drag grid
    private Vector3 lastMousePos;
    private bool isPanning = false;

    [Header("Color Settings")]
    public Color[] colors = new Color[7]
    {
        Color.red,
        Color.gray,
        Color.blue,
        Color.yellow,
        Color.magenta,
        Color.cyan,
        Color.green
    };

    [Range(1, 7)]
    public int currentColorIndex = 1;

    [HideInInspector]
    public int[,] gridData;

    private string saveFilePath => Path.Combine(Application.persistentDataPath, fileName + ".json");

    private void OnValidate()
    {
        //gridData = new int[rows, cols];
    }
    private void Start()
    {
        _camera = GameObject.FindObjectOfType<Camera>();
        gridData = new int[rows, cols];
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Camera.main == null) return;

        if (Input.GetMouseButtonDown(0)) isDraw = true;
        if (Input.GetMouseButtonUp(0)) isDraw = false;

        if (Input.GetMouseButtonDown(1)) isEmptyDraw = true;
        if (Input.GetMouseButtonUp(1)) isEmptyDraw = false;

        if (Input.GetMouseButtonDown(2))
        {
            isPanning = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2)) isPanning = false;

        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * 0.01f; // Điều chỉnh tốc độ
            Camera.main.transform.position += move;
            lastMousePos = Input.mousePosition;
        }

        // --- Zoom bằng con lăn chuột ---
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            float zoomSpeed = 15f;
            Camera.main.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1f, 100f);
        }

        if (!isDraw && !isEmptyDraw) return;

        if (isDraw == true || isEmptyDraw == true)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(worldPos.x / cellSize);
            int yy = Mathf.FloorToInt(worldPos.y / cellSize);
            int y = -yy;

            if (x >= 0 && x < cols && y >= 0 && y < rows)
            {
                if (isDraw)
                {
                    gridData[y, x] = currentColorIndex;
                }
                else if (isEmptyDraw)
                {
                    gridData[y, x] = 0;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gridData == null || gridData.GetLength(0) != rows || gridData.GetLength(1) != cols)
        {
            gridData = new int[rows, cols];
        }

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 pos = new Vector3(x * cellSize, -y * cellSize, 0);
                Gizmos.color = Color.gray;
                Gizmos.DrawWireCube(pos + Vector3.one * cellSize * 0.5f, Vector3.one * cellSize);

                int value = gridData[y, x];
                if (value >= 1 && value <= 7)
                {
                    Gizmos.color = colors[value - 1];
                    Gizmos.DrawCube(pos + Vector3.one * cellSize * 0.5f, Vector3.one * cellSize * 0.9f);
                }
            }
        }
    }

    [System.Serializable]
    public class GridSaveData
    {
        public List<int> data = new List<int>();
        public int rows;
        public int cols;
        public float _cameraSize;
    }

    public void SaveToJson()
    {
        GridSaveData saveData = new GridSaveData();
        saveData.rows = rows;
        saveData.cols = cols;
        float camSize = _camera.orthographicSize;
        saveData._cameraSize = camSize;

        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                saveData.data.Add(gridData[y, x]);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Grid saved to: " + saveFilePath);
    }

    public void LoadFromJson()
    {
        //if (File.Exists(saveFilePath))
        //{
        //    string json = File.ReadAllText(saveFilePath);
        //    GridSaveData saveData = JsonUtility.FromJson<GridSaveData>(json);

        //    rows = saveData.rows;
        //    cols = saveData.cols;
        //    gridData = new int[rows, cols];

        //    for (int i = 0; i < saveData.data.Count; i++)
        //    {
        //        int y = i / cols;
        //        int x = i % cols;
        //        gridData[y, x] = saveData.data[i];
        //    }

        //    Debug.Log("Grid loaded from: " + saveFilePath);
        //}
        string path = $"LevelJsons/{fileName}";
        TextAsset levelJson = Resources.Load<TextAsset>(path);

        if (levelJson == null)
        {
            Debug.LogError($"Không tìm thấy file: {path}");
            return;
        }
        GridSaveData saveData = JsonUtility.FromJson<GridSaveData> (levelJson.text);
        rows = saveData.rows;
        cols = saveData.cols;
        float camSize = saveData._cameraSize;
        _camera.orthographicSize = camSize;
        gridData = new int[rows, cols];
        for (int i = 0; i < saveData.data.Count; i++)
        {
            int y = i / cols;
            int x = i % cols;
            gridData[y, x] = saveData.data[i];
        }

        Debug.Log("Grid loaded from: " + path);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapCreateJson))]
public class GridPainterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapCreateJson gridPainter = (MapCreateJson)target;

        DrawDefaultInspector();

        GUILayout.Space(10);
        GUILayout.Label("Grid Painter Controls", EditorStyles.boldLabel);

        gridPainter.currentColorIndex = EditorGUILayout.IntSlider("Current Color (1–7)", gridPainter.currentColorIndex, 1, 7);

        GUI.backgroundColor = gridPainter.colors[0];
        if (GUILayout.Button("wall", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 1;
        }
        GUI.backgroundColor = gridPainter.colors[1];
        if (GUILayout.Button("start", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 2;
        }
        GUI.backgroundColor = gridPainter.colors[2];
        if (GUILayout.Button("enemy", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 3;
        }
        GUI.backgroundColor = gridPainter.colors[3];
        if (GUILayout.Button("fire", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 4;
        }
        GUI.backgroundColor = gridPainter.colors[4];
        if (GUILayout.Button("hammer", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 5;
        }
        GUI.backgroundColor = gridPainter.colors[5];
        if (GUILayout.Button("end", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 6;
        }
        GUI.backgroundColor = gridPainter.colors[6];
        if (GUILayout.Button("coin", GUILayout.Width(100), GUILayout.Height(30)))
        {
            gridPainter.currentColorIndex = 7;
        }

        GUI.backgroundColor = Color.white;

        if (GUILayout.Button("Save Grid to JSON"))
        {
            gridPainter.SaveToJson();
        }

        if (GUILayout.Button("Load Grid from JSON"))
        {
            gridPainter.LoadFromJson();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(gridPainter);
        }
    }
}
#endif
