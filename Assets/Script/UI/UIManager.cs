using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject UIMain_canvas;
    public GameObject UISelectLevel_canvas;


    // Start is called before the first frame update
    void Start()
    {
        StartUI();
    }

    void StartUI()
    {
        BackMain_canvas();
    }

    public void BackMain_canvas()
    {
        UIMain_canvas.SetActive(true);
        UISelectLevel_canvas.SetActive(false);
        SetupBackground(588f);
    }

    public void PlayGameBtn()
    {
        UIMain_canvas.SetActive(false);
        UISelectLevel_canvas.SetActive(true);
    }

    public void SetupBackground(float posX)
    {
        RectTransform rt = this.transform.GetChild(0).GetComponent<RectTransform>();
        Vector2 pos = rt.anchoredPosition;
        pos.x = posX;
        rt.anchoredPosition = pos;
    }

    public void PassLevel()
    {

    }

    public void FailLevel()
    {

    }
}