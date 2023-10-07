using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;
    GameManager gm;
    public string[] textReceived;
    public string[] allText;
    public bool textOpen;
    private int interactTimes;
    private int counter;
    private void Awake()
    {
        instance = this;
        textOpen = false;
        interactTimes = 0;
        counter = 0;
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    //formats text first if given as a paragraph
    public void displayText()
    {
        Debug.Log("Received text is: " + textReceived);
        //allText = textReceived.Split(new string[] { "\n\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        Debug.Log("Printing string now: ");
        foreach (string s in allText)
        {
            Debug.Log(s);
        }
        interactTimes = allText.Count();
        textOpen = true;
        showNextText();
    }

    public void showNextText() {
        Debug.Log("Counter is: " + counter);
        Debug.Log("interactTimes is: " + interactTimes);
        if (counter < interactTimes) {
            Debug.Log("Text running is: " + allText[counter]);
            counter++;
            return;
        }
        Debug.Log("After text finishes it should close itself");
        closeText();
    }

    public void closeText() {
        if (!textOpen) { return; }
        Debug.Log("Closing Text");
        textOpen=false;
        gm.textFinished = true;
        counter = 0;
    }
}
