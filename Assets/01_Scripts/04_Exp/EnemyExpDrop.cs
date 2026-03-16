using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyExpDrop : MonoBehaviour
{

    EnemyCore enemyCore;

    private void Awake()
    {
        enemyCore = GetComponent<EnemyCore>();
    }

    public void DropExp()
    {
        int expAmount = CalculateExp();
        if (expAmount <= 0)
        {
            return;
        }

        ExpOrbPool.Instance.Spawn(
            transform.position, expAmount );
    }

    int CalculateExp()
    {
        if (enemyCore.data == null)
        {
            return 0;
        }
        float minute = GameTimer.Instance.GetMinutes();

        switch (enemyCore.data.enemyType)
        {
            case EnemyType.Normal:
                if (minute < 3f) return 1;
                if (minute < 6f) return 2;
                return 3;

            case EnemyType.Elite:
                if (minute < 6f) return 25;
                if (minute < 9f) return 50;
                return 100;

            case EnemyType.Boss:
                return 300;
        }

        return 1;
    }

}
