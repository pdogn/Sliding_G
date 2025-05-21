using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] Button Exit;
    [SerializeField] Button Seting;
    [SerializeField] Button Home;
    [SerializeField] Button Resrart;

    private void Start()
    {
        Seting.onClick.AddListener(() => GameEvent.ShowSettingPanel());
        Exit.onClick.AddListener(ExitPopup);
        Home.onClick.AddListener(Home_Btn);
        Resrart.onClick.AddListener(Restart_Btn);
    }

    void ExitPopup()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.isPlayingTGamePlay = false;
    }

    void Home_Btn()
    {
        UIManager.Instance.ExecuteAcion(GameEvent.DisplayMainUI);
        this.gameObject.SetActive(false);
    }

    void Restart_Btn()
    {
        int crrLv = GameManager.Instance.CurrenLevel;
        GameEvent.ReplayLevel(crrLv);
        this.gameObject.SetActive(false);
    }

}
