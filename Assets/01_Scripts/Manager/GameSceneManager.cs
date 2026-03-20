using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    [SerializeField] string lobbyScene = "00_Lobby";
    [SerializeField] string stage_01 = "01_Stage01";
    [SerializeField] string stage_02 = "01_Stage02";

    bool isLoading;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void StartStage01()
    {
        LoadScene(stage_01);
    }

    public void StartStage02()
    {
        LoadScene(stage_02);
    }

    public void RestartGame()
    {
        LoadScene(stage_01);
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
