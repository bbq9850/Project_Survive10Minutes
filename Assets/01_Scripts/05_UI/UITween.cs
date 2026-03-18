using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITween : MonoBehaviour
{
    [SerializeField] float duration = 0.3f;
    [SerializeField] Ease ease = Ease.OutBack;

    CanvasGroup canvasGroup;
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void PlayOpen()
    {
        rect.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        rect.DOScale(1f, duration).SetEase(ease);
        canvasGroup.DOFade(1f, duration);
    }

    public void PlayClose(System.Action onComplete = null)
    {
        rect.DOScale(0f, duration).SetEase(Ease.InBack);
        canvasGroup.DOFade(0f, duration)
            .OnComplete(() => onComplete?.Invoke());
    }
}
