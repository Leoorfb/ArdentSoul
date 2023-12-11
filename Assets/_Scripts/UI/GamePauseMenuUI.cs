using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Classe que controla a UI do Menu de pause.
/// </summary>
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
        AudioManager.Instance.Play("MenuConfirm");
    }
    public void OnReturnButtonClick()
    {
        pauseManager.UnpauseGame();
        gameObject.SetActive(false);
        AudioManager.Instance.Play("MenuConfirm");
    }
    public void OnConfigButtonClick()
    {
        configMenu.SetActive(true);
        mainPauseMenu.SetActive(false);
        AudioManager.Instance.Play("MenuConfirm");
    }
}
