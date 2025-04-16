using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIMain_canvas;
    public GameObject UISelectLevel_canvas;

    [SerializeField] Transform content;
    [SerializeField] List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        StartUI();
        LoadButtonsLevel();
    }

    void StartUI()
    {
        UIMain_canvas.SetActive(true);
        UISelectLevel_canvas.SetActive(false);
    }

    void LoadButtonsLevel()
    {
        foreach(Transform item in content)
        {
            Button[] btns = item.GetComponentsInChildren<Button>(true);
            foreach (Button btn in btns)
            {
                buttons.Add(btn);
            }
        }

        for(int i=0; i< buttons.Count; i++)
        {
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {i + 1}";
        }
    }

}
