using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodModManager : MonoBehaviour
{
    public static GodModManager Instance;

    [Header("GodMode")]
    public bool godMode;

    [Header("Game Speed")]
    [Range(0.1f, 10f)]
    public float gameSpeed = 1f;
    public Text gameSpeedText;

    PlayerHealth playerHealth;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerHealth health = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
        gameSpeedText.text = $"현재 게임속도 : {gameSpeed:0}";
    }

    public void GodMod()
    {
        godMode = !godMode;
        Debug.Log("GodMode : " + godMode);
    }

    public void GodLevelUp()
    {
        int needExp = PlayerExp.Instance.ExpToNext - PlayerExp.Instance.CurrentExp;
        PlayerExp.Instance.AddExp(needExp);
    }

    public void SpeedUp()
    {
        gameSpeed += 1f;
    }

    public void SpeedDown()
    {
        gameSpeed -= 1f;

        if (gameSpeed < 0.1f)
            gameSpeed = 0.1f;
    }
}
