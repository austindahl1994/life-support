using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    GameManager gm;
    AudioManager am;

    public GameObject canvas;
    [SerializeField] private Image image;
    public GameObject settingsScreen;
    public GameObject noteScreen;

    [SerializeField] private Slider mainMusicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;
    public TMP_Text textMusic;
    public TMP_Text textSFX;
    public TMP_Text textAmb;

    private AudioSource audioSource;
    public AudioClip noteClip;

    public string date;
    public string para;
    public string noteName;

    [SerializeField] private TMP_Text textDate;
    [SerializeField] private TMP_Text textPara;
    [SerializeField] private TMP_Text textName;

    public float fadeDuration = 0.2f;
    public float fadeWaitTime = 0.05f;
    private bool uiOpen;
    private bool uiSettingsOpen;
    public GameObject screenFader;

    [SerializeField] private GameObject victoryImage;

    private void Awake()
    {
        instance = this;
        uiOpen = false;
        uiSettingsOpen = false;
        audioSource = GetComponent<AudioSource>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    private void Start()
    {
        gm = GameManager.instance;
        am = AudioManager.instance;
        if (image == null) { 
            image = GameObject.Find("ScreenFader").GetComponent<Image>();
        }

        fadeDuration = 0.2f;
        fadeWaitTime = 0.05f;

        if (settingsScreen == null) {
            settingsScreen = GameObject.Find("Canvas/Settings");
            settingsScreen.SetActive(false);
        }
        mainMusicSlider.value = 5.0f;
        sfxSlider.value = 5.0f;
        ambienceSlider.value = 3.0f;
        textMusic.text = (mainMusicSlider.value * 10).ToString() + "%";
        textSFX.text = (sfxSlider.value * 10).ToString() + "%";
        textAmb.text = (ambienceSlider.value * 10).ToString() + "%";
        mainMusicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
        ambienceSlider.onValueChanged.AddListener(OnAmbienceSliderChanged);
    }

    public IEnumerator FadeScreen()
    {
        Debug.Log("Fade screen called");
        // Fade to black
        float t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, t / fadeDuration);
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }

        // Wait for a moment
        yield return new WaitForSeconds(fadeWaitTime);

        if (gm.playerIsTeleporting) { 
            gm.teleportPlayer();
        }
        
        
        // Fade back in
        t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
            if (t >= fadeDuration / 2) { 
                gm.playerIsTeleporting = false;
            }
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
    }

    public IEnumerator fadeOut() {
        fadeDuration = 2.0f;
        float t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, t / fadeDuration);
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        victoryScreen();
        yield return null;
    }

    private void victoryScreen() {
        victoryImage.gameObject.SetActive(true);
    }

    public IEnumerator fadeIn() {
        float t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
            if (t >= fadeDuration / 2)
            {
                gm.sceneSwapping = false;
            }
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
    }

    public void openSettings() { 
        settingsScreen.SetActive(true);
        uiSettingsOpen = true;
    }

    public void closeSettings () {
        if (!uiSettingsOpen) { return; }
        settingsScreen.SetActive(false);
        uiSettingsOpen = false;
    }

    public void showNote() {
        textDate.text = date;
        textPara.text = para;
        textName.text = noteName;
        noteScreen.gameObject.SetActive(true);
        uiOpen = true;
        audioSource.clip = noteClip;
        audioSource.pitch = 1.3f;
        audioSource.Play();
    }

    public void closeNote() {
        if (!uiOpen) { return; }
        noteScreen.gameObject.SetActive(false);
        uiOpen = false;
    }

    private void OnMusicSliderChanged(float value) {
        mainMusicSlider.value = value;
        textMusic.text = (value* 10).ToString() + "%";
        am.setGlobalMusic(value / 10);
    }

    private void OnSFXSliderChanged(float value)
    {
        sfxSlider.value = value;
        textSFX.text = (value*10).ToString() + "%";
        am.setGlobalSFX(value / 10);
    }
    private void OnAmbienceSliderChanged(float value)
    {
        ambienceSlider.value = value;
        textAmb.text = (value* 10).ToString() + "%";
        am.setGlobalAmbience(value / 10);
    }
}
