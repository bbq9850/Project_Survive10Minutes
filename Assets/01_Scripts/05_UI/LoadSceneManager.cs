using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    public void OnClickStart()
    {
        GameSceneManager.Instance.StartStage01();
    }

    public void OnClickRestart()
    {
        GameSceneManager.Instance.RestartGame();
    }

    public void OnClickLobby()
    {
        GameSceneManager.Instance.GoToLobby();
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
