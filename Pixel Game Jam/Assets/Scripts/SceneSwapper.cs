using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwapper : MonoBehaviour
{
    InputManager im;
    SceneManage sm;
    GameManager gm;
    [SerializeField] private bool elevatorToFirst;
    [SerializeField] private bool elevatorToThird;
    [SerializeField] private bool stairsToSecond;
    [SerializeField] private bool stairsToThird;
    private void Start()
    {
        im = InputManager.instance;
        sm = SceneManage.instance;
        gm = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (elevatorToFirst) {
                sm.floorToLoad = "First Floor";
                gm.scenePlacePlayer = new Vector3 (0, 0, 0);
            } else if (elevatorToThird) {
                sm.floorToLoad = "Third Floor";
                gm.scenePlacePlayer = new Vector3(0, 0, 0);
            } else if (stairsToSecond) {
                sm.floorToLoad = "Second Floor";
                gm.scenePlacePlayer = new Vector3(21.5f, 0, 0);
            } else if (stairsToThird) {
                sm.floorToLoad = "Third Floor";
                gm.scenePlacePlayer = new Vector3(21.5f, 0, 0);
            }
            im.state = "scene";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            im.state = "none";
        }
    }
}
