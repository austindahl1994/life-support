using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rippedPieces : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector2 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }
    private void OnMouseDown()
    {
        Debug.Log("Mouse clicked object!");
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void resetPiecePosition() {
        transform.position = originalPosition;
    }
}
