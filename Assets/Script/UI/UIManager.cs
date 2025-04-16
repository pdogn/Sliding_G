using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIMain_canvas;
    public GameObject UISelectLevel_canvas;

    [Space(10)]
    [Header("Main_canvas")]
    [SerializeField] Button Play_btn;
    [SerializeField] Button Setting;
    [SerializeField] Button Description;
    [SerializeField] Button Facebok;
    [SerializeField] Button RemoveAds;

    [Space(10)]
    [Header("SelectLevel_canvas")]
    [SerializeField] Transform content;
    [SerializeField] List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        StartUI();

        Play_btn.onClick.AddListener(PlayGameBtn);
        Setting.onClick.AddListener(SettingBtn);
        Description.onClick.AddListener(DesBtn);
        Facebok.onClick.AddListener(FaebokBtn);
        RemoveAds.onClick.AddListener(NotAdsBtn);

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

    void PlayGameBtn()
    {
        UIMain_canvas.SetActive(false);
        UISelectLevel_canvas.SetActive(true);
    }

    void SettingBtn()
    {
        
    }
    void DesBtn()
    {
        
    }
    void FaebokBtn()
    {
        
    }
    void NotAdsBtn()
    {
        
    }

}
