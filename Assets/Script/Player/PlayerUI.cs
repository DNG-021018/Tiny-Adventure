using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] float levelLoadDelay = 0.015f;

    private void Start()
    {
        PausePanel.SetActive(false);
    }

    public void OnPause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnContinue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnHome()
    {
        string sceneName = "Level Scene";
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void OnRestart()
    {
        StartCoroutine(ReloadSceneCoroutine());
    }

    IEnumerator ReloadSceneCoroutine()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
