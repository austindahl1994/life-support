using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readerScript : MonoBehaviour
{
    public void animationDone() {
        Debug.Log("Animation finished script called");
        pinPad pinPad = gameObject.GetComponentInParent<pinPad>();
        pinPad.setBlack();
    }
}
