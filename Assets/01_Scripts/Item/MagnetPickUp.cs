using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ActivateMagnet();
        gameObject.SetActive(false);
    }

    void ActivateMagnet()
    {
        ExpOrbPool.Instance.ActivateMagnetAll();

        MagnetUI.Instance.Show();
    }
}
