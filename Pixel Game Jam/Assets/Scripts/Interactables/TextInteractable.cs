using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer sr;

    GameManager gm;
    TextManager tm;
    InputManager im;
    public DialogueObject textObj;
    [TextArea(5, 10)]
    public string textToShow;

    private void Start()
    {
        gm = GameManager.instance;
        tm = TextManager.instance;
        im = InputManager.instance;
        sr = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (textObj == null)
            {
                //tm.textReceived = textToShow;
            }
            else {
                tm.textReceived = textObj.dialogueText;
            }
            sr.color = new Color(133, 133, 133, 255);
            im.state = "text";
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
