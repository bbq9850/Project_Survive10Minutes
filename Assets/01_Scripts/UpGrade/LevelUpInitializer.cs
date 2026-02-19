using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInitializer : MonoBehaviour
{
    [SerializeField] PlayerExp playerExp;
    [SerializeField] LevelUpUI levelUpUI;

    void Start()
    {
        playerExp.OnLevelUp += levelUpUI.Open;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
