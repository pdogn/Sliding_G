using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
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

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartUI();

        Img_LevelPress = Resources.Load<Sprite>(Img_LevelPressPath);
        Img_LevelLocked = Resources.Load<Sprite>(Img_LevelLockedPath);
        //Main_canvas
        Play_btn.onClick.AddListener(PlayGameBtn);
        Setting.onClick.AddListener(SettingBtn);
        Description.onClick.AddListener(DesBtn);
        Facebok.onClick.AddListener(FaebokBtn);
        RemoveAds.onClick.AddListener(NotAdsBtn);

        //SelectLevel_canvas
        btn_backMainCanvas.onClick.AddListener(BackMain_canvas);
        btn_PlayNewLevel.onClick.AddListener(Play_New_Level);
        btn_Shop.onClick.AddListener(OpenShop);

        Load_UI_SelectLevel();
    }

    void StartUI()
    {
        BackMain_canvas();
    }

    void BackMain_canvas()
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

        levelManager.LoadLevelInfo();

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i=0; i< buttons.Count; i++)
        {
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelManager.GetLevelName(i); //$"{i + 1}";

            if (i < unlockedLevel)
            {
                buttons[i].GetComponent<Image>().sprite = Img_LevelPress;
                buttons[i].interactable = true;
                buttons[i].transform.GetChild(0).gameObject.SetActive(true);

                int index = i;
                buttons[i].onClick.AddListener(() => Event_PlayLevel(index));
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

    void Event_PlayLevel(int levelId)
    {
        UISelectLevel_canvas.SetActive(false);
        Debug.Log("Load level  " +  levelId);
        string levelPath = levelManager.GetPath(levelId);
        GridManager.Instance.LoadLevel(levelPath);
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

    void OpenShop()
    {

    }

    void Play_New_Level()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int LevelId = unlockedLevel - 1;
        Event_PlayLevel(LevelId);
    }
}
