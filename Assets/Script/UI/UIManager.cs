using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject UIMain_canvas;
    public GameObject UISelectLevel_canvas;
    public GameObject UI_Ingame;
    public Image UI_LoadScreenImg;

    public GameObject Pass_Fail_canvas;

    public GameObject PauseMenu_canvas;

    //GameObject backgroundImg;
    // Start is called before the first frame update
    void Start()
    {
        GameEvent.OnDisplayMainUI += BackMain_canvas;
        GameEvent.OnDisplaySelectLvUI += DisplaySelected_canvas;

        GameEvent.OnPassLevel += DisPlayPassLevelCanvas;
        GameEvent.OnFailLevel += DisPlayFailLevelCanvas;

        StartUI();
    }

    private void OnDisable()
    {
        GameEvent.OnDisplayMainUI -= BackMain_canvas;
        GameEvent.OnDisplaySelectLvUI -= DisplaySelected_canvas;
        GameEvent.OnPassLevel -= DisPlayPassLevelCanvas;
        GameEvent.OnFailLevel -= DisPlayFailLevelCanvas;
    }

    public void ExecuteAcion(Action action)
    {
        UI_LoadScreenImg.gameObject.SetActive(true);
        UI_LoadScreenImg.DOFade(1f, 0.7f).OnComplete(() =>
        {
            action();

            UI_LoadScreenImg.DOFade(0f, .2f).OnComplete(() => 
            {
                UI_LoadScreenImg.gameObject.SetActive(false);
            });
        });
    }

    void StartUI()
    {
        //BackMain_canvas();
        ExecuteAcion(GameEvent.DisplayMainUI);
    }

    private void BackMain_canvas()
    {
        UIMain_canvas.SetActive(true);
        UISelectLevel_canvas.SetActive(false);
        UI_Ingame.SetActive(false);
        SetupBackground(588f);
        GridManager.Instance.gameObject.SetActive(false);
    }

    private void DisplaySelected_canvas()
    {
        UIMain_canvas.SetActive(false);
        UISelectLevel_canvas.SetActive(true);
        UI_Ingame.SetActive(false);
    }

    public void PlayIngameUI()
    {
        UISelectLevel_canvas.SetActive(false);
        UI_Ingame.SetActive(true);
    }

    public void SetupBackground(float posX)
    {
        RectTransform rt = this.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 pos = rt.anchoredPosition;
        pos.x = posX;
        rt.anchoredPosition = pos;
        //if (backgroundImg == null)
        //{
        //    backgroundImg = GameObject.Find("Background");
        //}
        //Vector2 pos = backgroundImg.transform.position;
        //pos.x = posX;
        //backgroundImg.transform.position = pos;
    }

    public void DisPlayPassLevelCanvas()
    {
        Pass_Fail_canvas.SetActive(true);
        Pass_Fail_canvas.GetComponent<Pass_Fail_UI_Canvas>().ShowPassLevelUI();
        GameManager.Instance.isPlayingLevel = false;
    }

    public void DisPlayFailLevelCanvas()
    {
        Pass_Fail_canvas.SetActive(true);
        Pass_Fail_canvas.GetComponent<Pass_Fail_UI_Canvas>().ShowFailLevelUI();
        GameManager.Instance.isPlayingLevel = false;
    }
}