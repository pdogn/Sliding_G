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
    public GameObject UI_LoadScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameEvent.OnLoadScreen += DisplayScreenUI;
        StartUI();
    }

    private void OnDisable()
    {
        GameEvent.OnLoadScreen -= DisplayScreenUI;
    }

    void StartUI()
    {
        UI_LoadScreen.SetActive(false);
        BackMain_canvas();
    }

    public void BackMain_canvas()
    {
        DisplayScreenUI();
        UIMain_canvas.SetActive(true);
        UISelectLevel_canvas.SetActive(false);
        UI_Ingame.SetActive(false);
        SetupBackground(588f);
    }

    public void DisplaySelected_canvas()
    {
        DisplayScreenUI();
        UIMain_canvas.SetActive(false);
        UISelectLevel_canvas.SetActive(true);
        UI_Ingame.SetActive(false);
    }

    public void PlayIngameUI()
    {
        DisplayScreenUI();
        UISelectLevel_canvas.SetActive(false);
        UI_Ingame.SetActive(true);
    }

    public void SetupBackground(float posX)
    {
        RectTransform rt = this.transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 pos = rt.anchoredPosition;
        pos.x = posX;
        rt.anchoredPosition = pos;
    }

    void DisplayScreenUI()
    {
        UI_LoadScreen.SetActive(true);
    }

    public void PassLevel()
    {

    }

    public void FailLevel()
    {

    }
}