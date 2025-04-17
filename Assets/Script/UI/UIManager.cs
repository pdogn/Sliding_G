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

    private const string Img_LevelPressPath = "Images/lvl_block_pressed";
    private const string Img_LevelLockedPath = "Images/lvl_lok1";
    Sprite Img_LevelPress;
    Sprite Img_LevelLocked;

    private void Awake()
    {
        //int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartUI();

        Img_LevelPress = Resources.Load<Sprite>(Img_LevelPressPath);
        Img_LevelLocked = Resources.Load<Sprite>(Img_LevelLockedPath);

        Play_btn.onClick.AddListener(PlayGameBtn);
        Setting.onClick.AddListener(SettingBtn);
        Description.onClick.AddListener(DesBtn);
        Facebok.onClick.AddListener(FaebokBtn);
        RemoveAds.onClick.AddListener(NotAdsBtn);

        Load_UI_SelectLevel();
    }

    void StartUI()
    {
        UIMain_canvas.SetActive(true);
        UISelectLevel_canvas.SetActive(false);
    }

    void Load_UI_SelectLevel()
    {
        foreach (Transform item in content)
        {
            Button[] btns = item.GetComponentsInChildren<Button>(true);
            foreach (Button btn in btns)
            {
                buttons.Add(btn);
            }
        }

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i=0; i< buttons.Count; i++)
        {
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{i + 1}";

            if (i < unlockedLevel)
            {
                buttons[i].GetComponent<Image>().sprite = Img_LevelPress;
                buttons[i].interactable = true;
                buttons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                buttons[i].GetComponent<Image>().sprite = Img_LevelLocked;
                buttons[i].interactable = false;
                buttons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
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
