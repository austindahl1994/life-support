using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;
    public GameObject path;
    public Dictionary<Vector3Int, Vector3Int> tilePairs = new Dictionary<Vector3Int, Vector3Int>();
    public Vector3Int doorPos;
    Vector3 toBeMoved;
    GameManager gm;
    UIManager ui;
    SceneManage sm;
    public FloorPaths thirdFloor;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gm = GameManager.instance;
        ui = UIManager.instance;
        sm = SceneManage.instance;
        setPaths();
    }

    private void showPaths() { 
        if (!gm.showPaths) { return; }
        for (int i = 0; i < thirdFloor.path.Count(); i++) {
            GameObject testPath = Instantiate(path);
            testPath.gameObject.transform.GetChild(0).transform.position
                = new Vector2(thirdFloor.path[i].start.x, thirdFloor.path[i].start.y);
            testPath.gameObject.transform.GetChild(1).transform.position
                = new Vector2(thirdFloor.path[i].end.x, thirdFloor.path[i].end.y);
        }
    }

    public void teleport()
    {
        if (!gm.pinSolved) {
            return;
        }
        if (gm.playerIsTeleporting) {
            return;
        }
        if (!gm.hasLeftDoorway) {
            if (gm.whereToTeleportPlayer == doorPos)
            {
                gm.whereToTeleportPlayer = toBeMoved;
            }
            else {
                gm.whereToTeleportPlayer = doorPos;
            }
            StartCoroutine(teleportPause());
            return;
        }
        testing();
        /*
        if (tilePairs.ContainsKey(doorPos)) {
            toBeMoved = new Vector3(tilePairs[doorPos].x, tilePairs[doorPos].y, 0);
            gm.whereToTeleportPlayer = toBeMoved;
            StartCoroutine(teleportPause());
        } else if (tilePairs.ContainsValue(doorPos)) {
            foreach (KeyValuePair<Vector3Int, Vector3Int> pair in tilePairs) { 
                if (pair.Value == doorPos)
                {
                    toBeMoved = new Vector3(pair.Key.x, pair.Key.y, 0);
                    gm.whereToTeleportPlayer = toBeMoved;
                    StartCoroutine(teleportPause());
                }
            }
        }*/
    }

    private void testing() {
        for (int i = 0; i < thirdFloor.path.Count(); i++)
        {
            Debug.Log(doorPos.x+ " " +  doorPos.y);
            if (doorPos.x == thirdFloor.path[i].start.x && doorPos.y == thirdFloor.path[i].start.y) {
                toBeMoved = new Vector2(thirdFloor.path[i].end.x, thirdFloor.path[i].end.y);
                gm.whereToTeleportPlayer = toBeMoved;
                StartCoroutine(teleportPause());
            } else if (doorPos.x == thirdFloor.path[i].end.x && doorPos.y == thirdFloor.path[i].end.y) {
                toBeMoved = new Vector2(thirdFloor.path[i].start.x, thirdFloor.path[i].start.y);
                gm.whereToTeleportPlayer = toBeMoved;
                StartCoroutine(teleportPause());
            }
        }
    }

    private IEnumerator teleportPause()
    {
        gm.playerIsTeleporting = true;
        yield return StartCoroutine(ui.FadeScreen());
    }

    public void setPaths()
    {
        Debug.Log("Set paths called");
        tilePairs.Clear();
        //string sceneName = sm.getSceneName();
        /*
        switch (sceneName) {
            case "First Floor":
                //Debug.Log("It is first floor");
                currentFloor = firstFloor;
                break;
            case "Second Floor":
                //Debug.Log("It is second floor");
                currentFloor = secondFloor;
                break;
            case "Third Floor":
                //Debug.Log("It is third floor");
                currentFloor = thirdFloor;
                break;
            default:
                //Debug.Log("No floor currently set");
                break;
        }*/
        showPaths();
    }
}
