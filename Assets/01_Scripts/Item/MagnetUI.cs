using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetUI : MonoBehaviour
{
    public static MagnetUI Instance;

    [SerializeField] GameObject icon;
    [SerializeField] float showTime = 2f;

    void Awake()
    {
        Instance = this;
        icon.SetActive(false);
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        icon.SetActive(true);

        yield return new WaitForSeconds(showTime);

        icon.SetActive(false);
    }
}
