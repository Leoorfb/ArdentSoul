using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que muda a fase do jogo quando o player aciona.
/// </summary>
public class NextLevelEntrance : MonoBehaviour
{
    [SerializeField] string playSceneName;
    [SerializeField] protected LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerLayer == (1 << collision.gameObject.layer))
        {
            SceneManager.LoadScene(playSceneName);
        }
    }
}
