using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting_canvas : MonoBehaviour
{
    [SerializeField] Button Exit;
    [SerializeField] Button Save_btn;
    [SerializeField] Slider MusicSetting;
    [SerializeField] Slider EffSoundSetting;

    private void Start()
    {
        Exit.onClick.AddListener(ExitPopup);
        Save_btn.onClick.AddListener(Save_Btn);
    }

    void ExitPopup()
    {
        this.gameObject.SetActive(false);
        //GameManager.Instance.isPlayingTGamePlay = false;
    }

    void Save_Btn()
    {
        //int crrLv = GameManager.Instance.CurrenLevel;
        //GameEvent.ReplayLevel(crrLv);
        this.gameObject.SetActive(false);
    }
}
