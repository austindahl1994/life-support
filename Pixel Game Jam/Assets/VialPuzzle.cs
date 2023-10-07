using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialPuzzle : MonoBehaviour
{
    private GameObject childVial;
    GameManager gm;
    AudioManager am;
    [SerializeField] private List<int> correctCombo = new List<int>();
    [SerializeField] private List<int> currentList = new List<int>();
    [SerializeField] private List<GameObject> vials = new List<GameObject>();
    private bool listsMatch;
    private Color currentlyInVial;
    private Color mixedColor;
    private int counter;
    public bool isMixing;
    [SerializeField] private AudioClip wrong;
    [SerializeField] private AudioClip right;
    private float originalWrong;
    private float originalRight;
    private AudioSource audioSource;

    private void Awake()
    {
        counter = 0;
        isMixing = false;
    }
    private void Start()
    {
        gm = GameManager.instance;
        am = AudioManager.instance;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void addColor(Color colorAdded, int num) {
        if (isMixing) { return; }
        //Debug.Log("counter is at: " + counter);
        currentList.Add(num);
        //Debug.Log("Current list is count: " + currentList.Count);
        childVial = vials[counter / 2].gameObject;
        //check if the vial needs new color or to add them, based on counter mod 2
        if (counter % 2 == 0)
        {
            StartCoroutine(colorBlender(colorAdded, colorAdded, childVial));
        }
        else { 
            currentlyInVial = childVial.GetComponent<SpriteRenderer>().color;
            StartCoroutine(colorBlender(currentlyInVial, colorAdded, childVial));
        }
        //coroutine
    }

    public void check() {
        listsMatch = true;
        for (int i = 0; i < currentList.Count; i++) {
            if (currentList[i] != correctCombo[i])
            {
                listsMatch = false;
            }
        }
        if (listsMatch)
        {
            playCorrectSFX();
            Debug.Log("Lists match!");
            gm.vialsSolved = true;
            gm.vialPuzzleSolved();
            //call gm saying it is done, put next step on screen?

        }
        else {
            //Debug.Log("Lists dont match!");
            playErrorSFX();
            Animator[] childAnimators = this.GetComponentsInChildren<Animator>();
            SpriteRenderer[] srs = this.GetComponentsInChildren<SpriteRenderer>();
            foreach (Animator animator in childAnimators)
            {
                animator.SetInteger("count", 3);
            }
            foreach (SpriteRenderer sr in srs)
            {
                if (sr.tag != "button") { 
                    sr.color = Color.white;
                }
            }
            counter = 0;
            currentList.Clear();
        }
    }

    public IEnumerator colorBlender(Color initialColor, Color targetColor, GameObject cObject) {
        //Debug.Log("Target color is: " + targetColor);
        //Debug.Log("Initial Color is: " + initialColor);
        isMixing = true;
        cObject.GetComponent<Animator>().SetInteger("count", counter % 2);
        //cObject.GetComponent<SpriteRenderer>().color = new Color(targetColor.r, targetColor.g, targetColor.b, 1.0f);

        float duration = 2.0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float blendFactor = elapsedTime / duration;
            Color mixedColor = Color.Lerp(initialColor, targetColor, 0.5f);
            cObject.GetComponent<SpriteRenderer>().color = new Color(mixedColor.r, mixedColor.g, mixedColor.b, 1.0f);
        }
        
        yield return new WaitForSeconds(0.7f);
        if (counter < 7)
        {
            counter++;
        }
        else
        {
            check();
        }
        isMixing = false;
        yield return null;
    }

    private void playCorrectSFX()
    {
        if (gm.vialsSolved) { return; }
        audioSource.clip = right;
        if (audioSource.clip != null)
        {
            audioSource.pitch = 2.0f;
            audioSource.volume = 1 * am.globalSFXVolume;
            audioSource.Play();
        }
    }

    private void playErrorSFX()
    {
        audioSource.pitch = 1.0f;
        audioSource.clip = wrong;
        if (audioSource.clip != null)
        {
            audioSource.volume = 1 * am.globalSFXVolume;
            audioSource.Play();
        }
    }
}
