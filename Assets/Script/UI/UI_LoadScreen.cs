using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadScreen : MonoBehaviour
{
    [SerializeField] Image img;

    private void OnEnable()
    {
        //DOTween.Sequence()
        //    .Append(img.DOFade(1f, 0f))
        //    .AppendInterval(0.5f) //delay
        //    .Append(img.DOFade(0f, 0.5f))
        //    .SetEase(Ease.InOutQuad)
        //    .OnComplete(() => this.gameObject.SetActive(false));
    }
}
