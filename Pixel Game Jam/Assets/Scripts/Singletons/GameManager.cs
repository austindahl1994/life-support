using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject playerPrefab;
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    public BoxCollider2D playerBC;

    AudioManager am;

    public Vector3 whereToTeleportPlayer;
    public bool playerIsTeleporting;
    public bool showPaths;
    public bool playerCanMove;
    public bool isMovingright;
    public bool isFacingRight;
    public bool isInDoorway;
    public bool hasLeftDoorway;

    public bool pinSolved;
    public bool vialsSolved;
    [SerializeField] private GameObject door1;
    public bool computerSolved;
    [SerializeField] private GameObject door2;

    public List<int> currentComputers = new List<int>();
    public List<int> compCorrectCode = new List<int>();

    public bool textFinished;

    public Vector3 scenePlacePlayer;
    public bool sceneSwapping;
    public int currentFloor;

    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject corpse;
    private void Awake()
    {
        Debug.Log("Awake called");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        isInDoorway = false;
        isFacingRight = false;
        hasLeftDoorway = true;
        pinSolved = false;
        computerSolved = false;
        vialsSolved = false;
        textFinished = true;
    }

    private void Start()
    {
        am = AudioManager.instance;
        playerAnim = player.GetComponent<Animator>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerBC = player.GetComponent<BoxCollider2D>();
        compCorrectCode.Add(7);
        compCorrectCode.Add(4);
        compCorrectCode.Add(2);
        compCorrectCode.Add(6);
        turnOnComputer(1);
    }

    public void movingRight() {
        if (!playerCanMove) {
            return;
        }
        playerAnim.SetBool("Walking", true);
        playerSprite.flipX = true;
        isFacingRight =true;
        isMovingright=true;
    }

    public void movingLeft() {
        if (!playerCanMove)
        {
            return;
        }
        playerAnim.SetBool("Walking", true);
        playerSprite.flipX = false;
        isFacingRight = false;
        isMovingright = false;
    }

    public void notMoving() {
        playerAnim.SetBool("Walking", false);
        if (isFacingRight) { 
            playerSprite.flipX = true;
        }
    }

    public void teleportPlayer()
    {
        Debug.Log("Teleport called with v3:" + whereToTeleportPlayer);
        player.gameObject.transform.position = new Vector3(whereToTeleportPlayer.x + 0.5f, whereToTeleportPlayer.y, 0);
        hasLeftDoorway = false;
        playerIsTeleporting = false;
    }

    public void placePlayer() {
        playerBC.enabled = false;
        player.gameObject.transform.position = scenePlacePlayer;
        playerBC.enabled = true;
        sceneSwapping = false;
    }

    public void turnOnComputer(int num) {
        currentComputers.Add(num);
        checkComputerValues();
    }

    public void turnOffComputer(int num) {
        currentComputers.Remove(num);
        checkComputerValues();
    }

    public void vialPuzzleSolved()
    {
        corpse.gameObject.SetActive(false);
        door1.gameObject.SetActive(false);
    }

    public void checkComputerValues()
    {
        if (currentComputers.Count != 4) {
            return;
        }
        if (!(currentComputers.Contains(7) && currentComputers.Contains(4) &&
          currentComputers.Contains(6) && currentComputers.Contains(2)))
        {
            Debug.Log("Computers not matching yet");
            return;
        }
        Debug.Log("You've won the game!");
        door2.gameObject.SetActive(false);
        playCorrectSFX();
    }

    private void playCorrectSFX()
    {
        audioSource.clip = victory;
        if (audioSource.clip != null)
        {
            audioSource.pitch = 1.0f;
            audioSource.volume = 1 * am.globalSFXVolume;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
