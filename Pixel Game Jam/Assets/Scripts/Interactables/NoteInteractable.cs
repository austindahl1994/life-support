using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NoteInteractable : MonoBehaviour
{
    public string date;
    [TextArea(5, 10)]
    public string para;
    public string noteName;

    private Color originalColor;
    private SpriteRenderer sr;

    GameManager gm;
    UIManager ui;
    InputManager im;
    private void Start()
    {
        gm = GameManager.instance;
        ui = UIManager.instance;
        im = InputManager.instance;
        sr = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.color = new Color(133, 133, 133, 255);
            im.state = "note";
            ui.noteName = noteName;
            ui.date = date;
            ui.para = para;
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
