using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinButton : MonoBehaviour
{
    AudioManager am;

    public Animator anim;
    public bool canBePressed;
    pinPad pinPad;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private float originalVolume;
    private void Start()
    {
        am = AudioManager.instance;
        anim = GetComponent<Animator>();
        pinPad = GetComponentInParent<pinPad>();
        canBePressed = true;
        buttonAudioSource = GetComponent<AudioSource>();
        buttonAudioSource.clip = buttonClip;
        originalVolume = buttonAudioSource.volume;
    }
    private void OnMouseDown()
    {
        if (!canBePressed)
        {
            return;
        }
        anim.Play("numpadbuttons");

        if (buttonClip != null) {
            Debug.Log("Volume is: " + originalVolume);
            buttonAudioSource.volume = originalVolume * am.globalSFXVolume;
            buttonAudioSource.Play();
        }
        if (GameManager.instance.pinSolved) {
            return;
        }
        pinPad.buttonPushed(transform.GetSiblingIndex());
    }
}
