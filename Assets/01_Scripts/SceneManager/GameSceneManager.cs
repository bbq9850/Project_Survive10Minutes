using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    [SerializeField] string lobbyScene = "ProtoType_Lobby";
    [SerializeField] string mainScene = "ProtoType_Main";

    bool isLoading;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartMain()
    {
        LoadScene(mainScene);
    }

    public void RestartGame()
    {
        LoadScene(mainScene);
    }

    public void GoToLobby()
    {
        LoadScene(lobbyScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadScene(string sceneName)
    {
        if (isLoading) return;

        StartCoroutine(LoadRoutine(sceneName));
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        isLoading = true;

        Time.timeScale = 1f;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            yield return null;
        }

        isLoading = false;
    }
}
