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

    Tween scaleTween;
    Tween fadeTween;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    void KillTweens()
    {
        scaleTween?.Kill();
        fadeTween?.Kill();

        DOTween.Kill(rect);
        DOTween.Kill(canvasGroup);
    }

    public void PlayOpen()
    {
        KillTweens();

        rect.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        scaleTween = rect.DOScale(1f, duration)
            .SetEase(ease)
            .SetUpdate(true)
            .SetTarget(rect);

        fadeTween = canvasGroup.DOFade(1f, duration)
            .SetTarget(canvasGroup)
            .SetUpdate(true);
    }

    public void PlayClose(System.Action onComplete = null)
    {
        KillTweens();

        scaleTween = rect.DOScale(0f, duration)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .SetTarget(rect);

        fadeTween = canvasGroup.DOFade(0f, duration)
            .SetTarget(canvasGroup)
            .SetUpdate(true)
            .OnComplete(() => onComplete?.Invoke());
    }

    private void OnDestroy()
    {
        KillTweens();
    }
}
