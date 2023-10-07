using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer sr;

    GameManager gm;
    InventoryManager invm;
    InputManager im;

    private void Start()
    {
        gm = GameManager.instance;
        invm = InventoryManager.instance;
        im = InputManager.instance;
        sr = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.color = new Color(133, 133, 133, 255);
            im.state = "item";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.color = originalColor;
            im.state = "none";
        }
    }
}
