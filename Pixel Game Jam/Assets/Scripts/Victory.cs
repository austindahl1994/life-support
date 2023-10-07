using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    UIManager ui;
    Input im;
    GameManager gameManager;

    private void Start()
    {
        ui = UIManager.instance;
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.playerIsTeleporting = true;
            StartCoroutine(ui.fadeOut());
        }
    }
}
