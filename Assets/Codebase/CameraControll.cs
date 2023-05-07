using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraControll : MonoBehaviour
{
    public float zoomMin, zoomMax, zoomspeed;
    Camera myCamera;
    UiService ui;

    
    void Construct(UiService _uicontroller)
    {
        ui = _uicontroller;
    }
    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.y > 0 && 
            ui.ingameUi.activeSelf && 
            myCamera.orthographicSize <= zoomMax)
        {
            myCamera.orthographicSize += zoomspeed;
        }
        else if(Input.mouseScrollDelta.y < 0 && 
            ui.ingameUi.activeSelf && 
            myCamera.orthographicSize >= zoomMin)
        {
            myCamera.orthographicSize -= zoomspeed;
        }
    }
}
