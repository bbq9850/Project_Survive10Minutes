using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CartoonFX.CFXR_Effect;

public class PlayerHitEffect : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] GameObject hitImage;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.OnDamaged += OnHit;
        hitImage.SetActive(false);
    }

    void OnHit(float damage)
    {
        StopAllCoroutines(); // 중복 방지
        StartCoroutine(HitImageRoutine());
    }

    IEnumerator HitImageRoutine()
    {
        hitImage.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        hitImage.SetActive(false);
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDamaged -= OnHit;
    }
}
