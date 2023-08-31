using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    public float zoomMin, zoomMax, zoomspeed;
    Camera myCamera;
    UiService _uiService;

    [Inject]
    void Construct(UiService _uicontroller)
    {
        _uiService = _uicontroller;
    }
    private void Start() => myCamera = GetComponent<Camera>();

    private void Update()
    {
        if (AbleToZoomOut())
        {
            myCamera.orthographicSize += zoomspeed;
        }

        else if (AbleToZoomIn())
        {
            myCamera.orthographicSize -= zoomspeed;
        }
    }

    private bool AbleToZoomIn()
    {
        return Input.mouseScrollDelta.y < 0 &&
                    _uiService.ingameUi.activeSelf &&
                    myCamera.orthographicSize >= zoomMin;
    }

    private bool AbleToZoomOut()
    {
        return Input.mouseScrollDelta.y > 0
                    && _uiService.ingameUi.activeSelf
                    && myCamera.orthographicSize <= zoomMax;
    }
}
