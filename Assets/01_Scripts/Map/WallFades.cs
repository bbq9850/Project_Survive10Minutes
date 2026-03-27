using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WallFades : MonoBehaviour
{
    Renderer rend;
    Material mat;

    float currentAlpha = 0f;
    float targetAlpha = 0f;

    [Header("Fade")]
    public float fadeSpeed = 5f;
    public float maxAlpha = 0.5f;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    void Update()
    {
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

        Color color = mat.color;
        color.a = currentAlpha;
        mat.color = color;
    }

    public void SetAlphaByDistance(float distance, float maxDistance)
    {
        float t = 1f - Mathf.Clamp01(distance / maxDistance);
        targetAlpha = t * maxAlpha;
    }

    public void Hide()
    {
        targetAlpha = 0f;
    }
}
