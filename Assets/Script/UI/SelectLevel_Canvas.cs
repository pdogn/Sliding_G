using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel_Canvas : Singleton<SelectLevel_Canvas>
{
    [Header("SelectLevel_canvas")]
    [SerializeField] Button btn_backMainCanvas;
    [SerializeField] Button btn_PlayNewLevel;
    [SerializeField] Button btn_Shop;
    [SerializeField] Transform content;
    [SerializeField] List<Button> buttons;

    public int GetButtonsCount()
    {
        return buttons.Count;
    }

    private const string Img_LevelPressPath = "Images/lvl_block_pressed";
    private const string Img_LevelLockedPath = "Images/lvl_lok1";
    Sprite Img_LevelPress;
    Sprite Img_LevelLocked;

    LevelManager levelManager;

    protected override void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        Img_LevelPress = Resources.Load<Sprite>(Img_LevelPressPath);
        Img_LevelLocked = Resources.Load<Sprite>(Img_LevelLockedPath);
    }
    private void OnEnable()
    {
        Load_UI_SelectLevel();
    }

    void Start()
    {
        GameEvent.OnReplayLevel += Event_PlayLevel;
        GameEvent.OnPlayNextLevel += Play_New_Level;

        btn_backMainCanvas.onClick.AddListener(BackMain_Canvas);
        btn_PlayNewLevel.onClick.AddListener(Play_New_Level);
        btn_Shop.onClick.AddListener(OpenShop);

    }
    
    //private void OnDisable()
    //{
    //    //GameEvent.OnReplayLevel -= Event_PlayLevel;
    //    //GameEvent.OnPlayNextLevel -= Play_New_Level;
    //}
    private void OnApplicationQuit()
    {
        GameEvent.OnReplayLevel -= Event_PlayLevel;
        GameEvent.OnPlayNextLevel -= Play_New_Level;
    }

    void Load_UI_SelectLevel()
    {
        foreach (Transform item in content)
        {
            Button[] btns = item.GetComponentsInChildren<Button>(true);
            foreach (Button btn in btns)
            {
                if (!buttons.Contains(btn))
                {
                    buttons.Add(btn);
                }
            }
        }

        levelManager.LoadLevelInfo();

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelManager.GetLevelName(i); //$"{i + 1}";

            if (i < unlockedLevel)
            {
                buttons[i].GetComponent<Image>().sprite = Img_LevelPress;
                buttons[i].interactable = true;
                buttons[i].transform.GetChild(0).gameObject.SetActive(true);

                int index = i;
                buttons[i].onClick.AddListener(() => Event_PlayLevel(index+1));
            }
            else
            {
                buttons[i].GetComponent<Image>().sprite = Img_LevelLocked;
                buttons[i].interactable = false;
                buttons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    void BackMain_Canvas()
    {
        //UIManager.Instance.BackMain_canvas();
        UIManager.Instance.ExecuteAcion(GameEvent.DisplayMainUI);
    }

    void Event_PlayLevel(int levelId)
    {
        Debug.Log("Load level  " + levelId);
        GameManager.Instance.CurrenLevel = levelId;
        ////string levelPath = levelManager.GetPath(levelId);
        //GridManager.Instance.LoadLevel(levelId);
        //UIManager.Instance.SetupBackground(-455f);
        //UIManager.Instance.PlayIngameUI();
        UIManager.Instance.ExecuteAcion(() =>
        {
            GridManager.Instance.gameObject.SetActive(true);
            GridManager.Instance.LoadLevel(levelId);
            UIManager.Instance.SetupBackground(-455f);
            UIManager.Instance.PlayIngameUI();
        });
    }
    void Play_New_Level()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int LevelId = unlockedLevel;
        Event_PlayLevel(LevelId);
    }

    void OpenShop()
    {

    }
}
