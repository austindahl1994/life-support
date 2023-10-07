using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private GameObject camObj;
    [SerializeField] private Camera cam;
    GameManager gm;
    
    private void Awake()
    {
        instance = this;
        camObj = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();
    }

    private void Start()
    {
        gm = GameManager.instance;
        cam.transform.position = new Vector3(gm.player.transform.position.x, gm.player.transform.position.y, -10);
    }

    private void Update()
    {
        cam.gameObject.transform.position = new Vector3 (gm.player.transform.position.x, gm.player.transform.position.y, -10);
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = cam.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Object was clicked
                GameObject clickedObject = hit.collider.gameObject;

                // Access the BoxCollider component if needed
                BoxCollider2D boxCollider = hit.collider.GetComponent<BoxCollider2D>();

                // Perform actions or checks based on the clicked object
                // ...

                Debug.Log("Clicked on: " + clickedObject.name);
            }
        }*/
    }
}
