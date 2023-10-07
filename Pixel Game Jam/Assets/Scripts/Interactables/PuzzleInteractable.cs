using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleInteractable : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject holderGameObject;
    private Color originalColor;
    private SpriteRenderer sr;

    GameManager gm;
    InputManager im;
    PuzzleManager pm;

    private void Awake()
    {
        if (puzzle == null) {
            puzzle = holderGameObject;
        }
        puzzle = Instantiate(puzzle);
        puzzle.SetActive(false);
    }
    private void Start()
    {
        gm = GameManager.instance;
        pm = PuzzleManager.instance;
        im = InputManager.instance;
        sr = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            pm.puzzleInt = this.gameObject;
            pm.puzzle = puzzle;
            sr.color = new Color(133, 133, 133, 255);
            im.state = "puzzle";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pm.puzzleInt = this.gameObject;
            pm.puzzle = puzzle;
            sr.color = new Color(133, 133, 133, 255);
            im.state = "puzzle";
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
