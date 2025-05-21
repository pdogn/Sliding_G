using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Canvas : MonoBehaviour
{
    [Header("Main_canvas")]
    [SerializeField] Button Play_btn;
    [SerializeField] Button Setting;
    [SerializeField] Button Description;
    [SerializeField] Button Facebok;
    [SerializeField] Button RemoveAds;

    private const string Setting_canvas_Path = "Prefabs/UI/Setting_canvas";

    private void Awake()
    {
        GameEvent.ClickSetting += ShowSettingPanel;
    }
    private void Start()
    {
        Play_btn.onClick.AddListener(PlayGameBtn1);
        Setting.onClick.AddListener(() => GameEvent.ShowSettingPanel());
        Description.onClick.AddListener(DesBtn);
        Facebok.onClick.AddListener(FaebokBtn);
        RemoveAds.onClick.AddListener(NotAdsBtn);
    }

    void PlayGameBtn1()
    {
        //UIManager.Instance.DisplaySelected_canvas();
        UIManager.Instance.ExecuteAcion(GameEvent.DisplaySelectLvUI);
    }
    void SettingBtn()
    {
        //GameEvent.ShowSettingPanel();
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

    private void ShowSettingPanel()
    {
        if (UIManager.Instance.Setting_canvas == null)
        {
            GameObject SettingCanvas_Prefab = Resources.Load<GameObject>(Setting_canvas_Path);
            GameObject go = Instantiate(SettingCanvas_Prefab, this.transform.parent);
            UIManager.Instance.Setting_canvas = go;
        }
        UIManager.Instance.Setting_canvas.SetActive(true);
        //GameManager.Instance.isPlayingTGamePlay = false;
    }
}
