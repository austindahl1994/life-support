using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class computer : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private Light2D light2d;
    public bool playerByComputer;
    private bool compTurnedOn;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
        setDefaultState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerByComputer == true) {
            swapComputerState();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerByComputer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerByComputer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerByComputer = false;
    }

    public void swapComputerState() {
        if (compTurnedOn)
        {
            compTurnedOn = false;
            light2d.intensity = 0.0f;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gm.turnOffComputer(number);
        }
        else {
            compTurnedOn=true;
            light2d.intensity = 3.0f;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gm.turnOnComputer(number);
        }
    }

    private void setDefaultState() {
        if (number == 1) {
            compTurnedOn = true;
            return;
        }
        compTurnedOn = false;
        light2d.intensity = 0.0f;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}
