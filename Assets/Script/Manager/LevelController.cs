using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 0.015f;
    [SerializeField] Button[] levelObject;
    public int unlockedLevel;
    public int currentLevels;

    private void Awake()
    {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevels", 1);
        currentLevels = PlayerPrefs.GetInt("CurrentLevels", 1);
        // PlayerPrefs.DeleteKey("UnlockedLevels");
        // PlayerPrefs.DeleteKey("CurrentLevels");

        for (int i = 0; i < levelObject.Length; i++)
        {
            levelObject[i].interactable = false;
            if (unlockedLevel > i)
            {
                UnlockedLevels();
                levelObject[i].interactable = true;
            }
        }
    }

    public void UnlockedLevels()
    {
        if (FindObjectOfType<GameManager>().GetUnlockedLevels() == true)
        {
            if (currentLevels == unlockedLevel)
            {
                unlockedLevel++;
                PlayerPrefs.SetInt("UnlockedLevels", unlockedLevel);
            }
        }
        else return;
    }

    public void OpenLevel(int levelId)
    {
        currentLevels = levelId;
        PlayerPrefs.SetInt("CurrentLevels", currentLevels);
        StartCoroutine(LoadLevel(levelId));
    }

    IEnumerator LoadLevel(int level_id)
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        string sceneName = $"Level_{level_id}";
        SceneManager.LoadScene(sceneName);
    }

    public void OnReturn()
    {
        StartCoroutine(LoadBackScene());
    }

    IEnumerator LoadBackScene()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex - 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
