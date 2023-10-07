using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneCaller : MonoBehaviour
{
    GameManager gm;
    DoorManager dm;
    void Start()
    {
        gm = GameManager.instance;
        dm = DoorManager.instance;
        dm.setPaths();
        gm.placePlayer();
    }
}
