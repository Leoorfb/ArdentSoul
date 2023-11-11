using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject configMenu;
    [SerializeField] GameObject mainPauseMenu;

    [SerializeField] Button resumeButton;
    [SerializeField] Button configButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] string mainMenuSceneName;

    PauseManager pauseManager;

    private void Start()
    {
        pauseManager = PauseManager.Instance;

        resumeButton.onClick.AddListener(OnReturnButtonClick);
        configButton.onClick.AddListener(OnConfigButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
    }

    private void OnEnable()
    {
        mainPauseMenu.SetActive(true);
        configMenu.SetActive(false);
    }

    public void OnMainMenuButtonClick()
    {
        pauseManager.UnpauseGame();
        SceneManager.LoadScene(0);
    }
    public void OnReturnButtonClick()
    {
        pauseManager.UnpauseGame();
        gameObject.SetActive(false);
    }
    public void OnConfigButtonClick()
    {
        configMenu.SetActive(true);
        mainPauseMenu.SetActive(false);
    }
}
