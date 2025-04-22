using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager instance;

    public GameObject LoadScene_canvas;
    public Slider progressBar;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        LoadScene_canvas.SetActive(true);
        StartCoroutine(SwitchToSceneAsync(1));
    }

    void SwitchToScene(int sceneId)
    {
        progressBar.value = 0;
    }

    IEnumerator SwitchToSceneAsync(int id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        while(!asyncLoad.isDone)
        {
            progressBar.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        LoadScene_canvas.SetActive(false);
    }
}
