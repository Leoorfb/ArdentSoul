using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject ConfigMenu;
    [SerializeField] Button playButton;
    [SerializeField] string playSceneName;
    [SerializeField] Button configButton;
    [SerializeField] Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        configButton.onClick.AddListener(OnConfigButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void OnConfigButtonClick()
    {
        ConfigMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
