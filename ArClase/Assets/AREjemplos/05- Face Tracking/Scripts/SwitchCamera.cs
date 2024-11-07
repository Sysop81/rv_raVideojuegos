using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SwitchCamera : MonoBehaviour
{

    private ARCameraManager _arCameraManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _arCameraManager = GetComponent<ARCameraManager>();
    }

    public void ChangeToFront()
    {
        _arCameraManager.requestedFacingDirection = CameraFacingDirection.User;
    }
    
    public void ChangeToBack()
    {
        _arCameraManager.requestedFacingDirection = CameraFacingDirection.World;
    }
}
