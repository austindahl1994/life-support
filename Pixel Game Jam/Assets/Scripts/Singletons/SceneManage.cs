using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;
    public string floorToLoad;

    private void Awake()
    {
        instance = this;
    }

    public string getSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    public void loadScene() {
        SceneManager.LoadScene(floorToLoad);
    }
}
