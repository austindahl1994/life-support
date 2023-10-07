using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private Rigidbody2D playerRB;
    GameManager gm;
    DoorManager dm;
    PuzzleManager pm;
    UIManager ui;
    TextManager tm;
    InventoryManager invm;
    SceneManage sm;
    public string state;
    public bool inInteraction;
    public bool inSettings;
    public bool goingThroughTextBox;

    [SerializeField] private float moveSpeed = 5;
    private float horizontalInput;
    private void Awake()
    {
        instance = this;
        inSettings = false;
        goingThroughTextBox = false;
    }

    private void Start()
    {
        gm = GameManager.instance;
        dm = DoorManager.instance;
        pm = PuzzleManager.instance;
        ui = UIManager.instance;    
        tm = TextManager.instance;
        invm = InventoryManager.instance;
        sm = SceneManage.instance;
        playerRB = gm.player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (gm.textFinished) {
            goingThroughTextBox = false;
        }
        if (inInteraction || gm.playerIsTeleporting || inSettings || goingThroughTextBox || gm.sceneSwapping)
        {
            gm.playerCanMove = false;
        }
        else {
            gm.playerCanMove = true;
        }
        if (horizontalInput > 0) {
            gm.movingRight();
        } else if (horizontalInput < 0) {
            gm.movingLeft();
        } else {
            gm.notMoving();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            interact();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            exit();
        }

    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer() {
        if (gm != null && gm.playerCanMove) { 
            Vector2 movement = new Vector2 (horizontalInput, 0);
            movement.Normalize();
            movement *= moveSpeed;
            playerRB.MovePosition(playerRB.position + movement * Time.fixedDeltaTime);
        }
    }

    private void interact() {
        if (inSettings) {
            Debug.Log("Settings is currently running, cannot interact");
            return; 
        }
        if (goingThroughTextBox) {
            tm.showNextText();
        }
        if (inInteraction)
        {
            closeAll();
            inInteraction = false;
        }
        else { 
            checkAvailableInteractable();
        }
    }

    private void exit() {
        if (inSettings) { 
            ui.closeSettings();
            inSettings = false;
        } else if (inInteraction)
        {
            closeAll();
        }
        else { 
            ui.openSettings();
            inSettings = true;
        }
    }

    private void closeAll() { 
        ui.closeNote();
        pm.endPuzzle();
        invm.closeInventory();
        inInteraction = false;
    }

    private void checkAvailableInteractable() {
        switch (state) {
            case "note":
                ui.showNote();
                inInteraction = true;
                break;
            case "puzzle":
                pm.startPuzzle();
                inInteraction = true;
                break;
            case "text":
                gm.textFinished = false;
                tm.displayText();
                goingThroughTextBox = true;
                break;
            case "item":
                invm.addToInventory();
                inInteraction = true;
                break;
            case "door":
                dm.teleport();
                break;
            case "none":
                Debug.Log("No interactable available");
                break;
            default:
                Debug.Log("Nothing available yet");
                break;
        }
    }
}
