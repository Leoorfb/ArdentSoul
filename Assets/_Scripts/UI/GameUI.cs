using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que controla a UI do Jogo.
/// </summary>
public class GameUI : Singleton<GameUI>
{
    public TextMeshProUGUI hpText;
    public string StartSceneName;
    public GameObject deathScreen;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        hpText.text = Player.Instance.playerData.health.ToString();
    }

    public void OnRetryButtonClick()
    {
        PauseManager.Instance.UnpauseGame();
        SceneManager.LoadScene(StartSceneName);
        AudioManager.Instance.Play("MenuConfirm");
    }
    public void OnMainMenuButtonClick()
    {
        PauseManager.Instance.UnpauseGame();
        SceneManager.LoadScene(0);
        AudioManager.Instance.Play("MenuConfirm");
    }

    public void OnPlayerDeath()
    {
        PauseManager.Instance.PauseGame();
        deathScreen.SetActive(true);
    }

    public void OnPlayerWin()
    {
        PauseManager.Instance.PauseGame();
        winScreen.SetActive(true);
    }
}
