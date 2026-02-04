using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{

    [SerializeField] float invinvibleTime = 0.5f;

    private bool isInvicible;
    public bool IsInvicible => isInvicible;

    public void Activate()
    {
        if (IsInvicible) return;
        StartCoroutine(InvicibleRoutine());
    }

    IEnumerator InvicibleRoutine()
    {
        isInvicible = true;
        yield return new WaitForSeconds(invinvibleTime);
        isInvicible = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
