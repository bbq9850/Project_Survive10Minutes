using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] Canvas canvas;

    void Awake()
    {
        Instance = this;
    }

    public void Spawn(Vector3 worldPos, float damage)
    {
        if (damageTextPrefab == null || canvas == null)
        {
            Debug.LogError("Prefab 또는 Canvas 없음");
            return;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject obj = Instantiate(damageTextPrefab, canvas.transform);

        obj.GetComponent<RectTransform>().position = screenPos;

        DamageText text = obj.GetComponent<DamageText>();
        text.Init(damage);
    }
}
