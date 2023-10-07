using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    public Vector2 doorStart;
    public Vector2 doorEnd;
    public LineRenderer lineRenderer;

    private void Start()
    {
        doorStart = transform.GetChild(0).transform.position;
        doorEnd = transform.GetChild(1).transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, doorStart);
        lineRenderer.SetPosition(1, doorEnd);
    }
}
