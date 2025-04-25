using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pass_Fail_UI_Canvas : MonoBehaviour
{
    public TextMeshProUGUI Lable;
    public Button btn_Home;
    public Button btn_RePlay;
    public Button btn_Next;

    private void Start()
    {
        btn_Home.onClick.AddListener(() => 
        {
            UIManager.Instance.ExecuteAcion(GameEvent.DisplayMainUI);
            this.gameObject.SetActive(false);
        });
        btn_RePlay.onClick.AddListener(() => {
            int crrLv = GameManager.Instance.CurrenLevel;
            GameEvent.ReplayLevel(crrLv);
            this.gameObject.SetActive(false);
        });
        btn_Next.onClick.AddListener(() => 
        {
            int crrLv = GameManager.Instance.CurrenLevel;
            GameEvent.ReplayLevel(crrLv+1);
            this.gameObject.SetActive(false);
        });
    }

    public void ShowPassLevelUI()
    {
        btn_Next.gameObject.SetActive(true);
        Lable.text = "Passed";
    }

    public void ShowFailLevelUI()
    {
        btn_Next.gameObject.SetActive(false);
        Lable.text = "Fail";
    }
}
