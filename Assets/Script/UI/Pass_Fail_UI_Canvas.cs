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

    [SerializeField] GameObject img_win;
    [SerializeField] GameObject img_fail;
    private void Start()
    {
        btn_Home.onClick.AddListener(() => 
        {
            UIManager.Instance.ExecuteAcion(GameEvent.DisplayMainUI);
            this.gameObject.SetActive(false);
        });
        btn_RePlay.onClick.AddListener(() => {
            int crrLv = GameManager.Instance.CurrenLevel;
            GameEvent.PlayLevel(crrLv);
            this.gameObject.SetActive(false);
        });
        btn_Next.onClick.AddListener(() => 
        {
            int crrLv = GameManager.Instance.CurrenLevel;
            GameEvent.PlayLevel(crrLv+1);
            this.gameObject.SetActive(false);
        });
    }

    public void ShowPassLevelUI()
    {
        btn_Next.gameObject.SetActive(true);
        Lable.text = "Passed";
        img_win.SetActive(true);
        img_fail.SetActive(false);
    }

    public void ShowFailLevelUI()
    {
        btn_Next.gameObject.SetActive(false);
        Lable.text = "Fail";
        img_win.SetActive(false);
        img_fail.SetActive(true);
    }
}
