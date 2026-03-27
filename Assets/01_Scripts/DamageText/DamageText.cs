using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    [SerializeField] float moveY = 110f;
    [SerializeField] float duration = 0.5f;

    RectTransform rect;
    Text text;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    public void Init(float damage)
    {
        text.text = ((int)damage).ToString();

        rect.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(rect.DOScale(1.3f, 0.2f).SetEase(Ease.OutBack));
        seq.Append(rect.DOScale(1f, 0.1f));

        seq.Join(rect.DOAnchorPosY(rect.anchoredPosition.y + 110f, 1f));
        seq.Join(text.DOFade(0f, 1f));

        seq.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

}
