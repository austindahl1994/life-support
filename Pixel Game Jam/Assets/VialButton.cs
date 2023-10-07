using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialButton : MonoBehaviour
{
    [SerializeField] Color currentColor;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }
    private void OnMouseDown()
    {
        if (gm.vialsSolved) { return;  }
        //Debug.Log("Button pressed for color" + currentColor);
        gameObject.GetComponentInParent<VialPuzzle>().addColor(currentColor, gameObject.transform.GetSiblingIndex());
    }
}
