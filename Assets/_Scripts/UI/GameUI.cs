using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Classe que controla a UI do Jogo.
/// </summary>
public class GameUI : Singleton<GameUI>
{
    public TextMeshProUGUI hpText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        hpText.text = Player.Instance.playerData.health.ToString();
    }
}
