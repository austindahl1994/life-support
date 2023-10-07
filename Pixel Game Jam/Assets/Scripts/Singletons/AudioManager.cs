using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip mainMusicClip;

    public AudioClip ambienceClip1;
    public AudioClip ambienceClip2;
    public AudioClip ambienceClip3;

    private AudioSource mmAudioSource;
    private AudioSource ambAudioSource;

    public float globalMusicVolume;
    public float globalSFXVolume;
    public float globalAmbienceVolume;

    private float finalMediumValue = 0.5f;
    private void Awake()
    {
        instance = this;
        globalMusicVolume = finalMediumValue;
        globalSFXVolume = finalMediumValue;
        globalAmbienceVolume = 0.3f;
        mmAudioSource = gameObject.AddComponent<AudioSource>();
        ambAudioSource = gameObject.AddComponent<AudioSource>();

    }

    private void Start()
    {
        if (mainMusicClip == null) {
            Debug.Log("Audio is null");
            mainMusicClip = Resources.Load<AudioClip>("Audio/mainMusic");
        }
        playMainMusic();
        playAmbienceMusic();
    }

    private void playMainMusic() {
        mmAudioSource.volume = globalMusicVolume;
        mmAudioSource.clip = mainMusicClip;
        mmAudioSource.loop = true;
        mmAudioSource.Play();
    }

    private void playAmbienceMusic() {
        //after fade in for volume
        ambAudioSource.volume = globalAmbienceVolume;
        ambAudioSource.clip = ambienceClip1;
        ambAudioSource.loop = true;
        ambAudioSource.Play();
    }

    private void setAmbienceClip(AudioClip tobeplayed) { 
        
    }

    public void stopAmbience() {
        if (ambAudioSource.clip != null) {
            ambAudioSource.Stop(); //set up fade out
        }
    }

    public void setGlobalMusic(float amount) {
        amount = Mathf.Clamp01(amount);
        globalMusicVolume = amount;
        mmAudioSource.volume = globalMusicVolume;
    }

    public void setGlobalSFX(float amount)
    {
        amount = Mathf.Clamp01(amount);
        globalSFXVolume = amount;
    }

    public void setGlobalAmbience(float amount)
    {
        amount = Mathf.Clamp01(amount);
        globalAmbienceVolume = amount;
        ambAudioSource.volume = globalAmbienceVolume;
    }
}
