using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWarningUI : MonoBehaviour
{
    public static BossWarningUI Instance;

    [SerializeField] Image redFlash;
    [SerializeField] Text warningText;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void ShowWarning(string text)
    {
        gameObject.SetActive(true);
        StartCoroutine(WarningRoutine(text));
    }

    IEnumerator WarningRoutine(string text)
    {
        gameObject.SetActive(true);

        warningText.text = text;


        for (int i = 0; i < 3; i++)
        {
            redFlash.color = new Color(1, 0, 0, 0.6f);
            yield return new WaitForSeconds(0.25f);

            redFlash.color = new Color(1, 0, 0, 0f);
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}
