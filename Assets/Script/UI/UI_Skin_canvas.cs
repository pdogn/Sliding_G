using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Skin_canvas : MonoBehaviour
{
    public Dictionary<string, ItemSkinUI> ItemSkins = new Dictionary<string, ItemSkinUI>();


    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();

        if (canvas.worldCamera == null)
        {
            Camera mainCam = Camera.main;

            if (mainCam != null)
            {
                canvas.worldCamera = mainCam;
                Debug.Log("Canvas camera has been assigned to Camera.main.");
            }
            else
            {
                Debug.LogWarning("No main camera found in the scene. Please tag your camera as 'MainCamera'.");
            }
        }
    }

    private void Update()
    {
        
    }

    public void AddDic(string key, ItemSkinUI value)
    {
        if (!ItemSkins.ContainsKey(key))
        {
            ItemSkins[key] = value;
        }
    }
}
