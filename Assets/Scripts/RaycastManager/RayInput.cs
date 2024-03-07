using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RayInput : MonoBehaviour
{
    public bool isMouseDown;
    public bool isMouseMove;
    public Vector2 worldMousePosition;
    public Vector2 mousePosition;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    public void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
        worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = Input.mousePosition;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0 || mouseY != 0)
        {
            isMouseMove = true;
        }
        else
        {
            isMouseMove = false;
        }
    }
}
