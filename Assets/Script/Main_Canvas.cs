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

    private void Start()
    {
        Play_btn.onClick.AddListener(PlayGameBtn1);
        Setting.onClick.AddListener(SettingBtn);
        Description.onClick.AddListener(DesBtn);
        Facebok.onClick.AddListener(FaebokBtn);
        RemoveAds.onClick.AddListener(NotAdsBtn);
    }

    void PlayGameBtn1()
    {
        UIManager.Instance.PlayGameBtn();
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
