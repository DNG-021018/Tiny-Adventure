using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] PlayerDatabase playerDatabase;
    [SerializeField] TextMeshProUGUI nameText;
    public int selectedOption = 0;
    [SerializeField] Image image;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject HomeMenu;
    [SerializeField] GameObject char_1;
    [SerializeField] GameObject char_2;
    [SerializeField] GameObject char_3;
    [SerializeField] GameObject char_4;
    [SerializeField] float levelLoadDelay = 0.015f;

    void Start()
    {
        UpdateCharacter(selectedOption);
        HomeMenu.SetActive(true);
        StartMenu.SetActive(false);
        char_1.SetActive(true);
        char_2.SetActive(true);
        char_3.SetActive(true);
        char_4.SetActive(true);
    }

    void Update()
    {
        OnEcs();
    }

    private void SetCharacterVisibility(bool visible)
    {
        char_1.SetActive(visible);
        char_2.SetActive(visible);
        char_3.SetActive(visible);
        char_4.SetActive(visible);
    }

    public void OnNext()
    {
        selectedOption++;
        if (selectedOption >= playerDatabase.GetPlayerLength())
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
    }

    public void OnBack()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = playerDatabase.GetPlayerLength() - 1;
        }
        UpdateCharacter(selectedOption);
    }

    public void OnStartClick()
    {
        StartMenu.SetActive(true);
        HomeMenu.SetActive(false);
        SetCharacterVisibility(false);
    }

    public void OnTurnBackClick()
    {
        StartMenu.SetActive(false);
        HomeMenu.SetActive(true);
        SetCharacterVisibility(true);
    }

    public void OnEcs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartMenu.SetActive(false);
            HomeMenu.SetActive(true);
            SetCharacterVisibility(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateCharacter(int selectedOption)
    {
        Player_SO player_SO = playerDatabase.GetPlayerIndex(selectedOption);
        nameText.text = player_SO.characterName;
        image.sprite = player_SO.characterSprite;
    }

    public void OnCompelete()
    {
        FindObjectOfType<GameManager>().SetSkinIndex(selectedOption);
        StartMenu.SetActive(false);
        HomeMenu.SetActive(true);
        SetCharacterVisibility(true);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}