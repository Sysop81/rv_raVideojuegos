using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SwitchCamera : MonoBehaviour
{

    private ARCameraManager _arCameraManager;
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _arCameraManager = GetComponent<ARCameraManager>();
    }
    
    /// <summary>
    /// Method ChangeToFront
    /// This method change to user camera
    /// </summary>
    public void ChangeToFront()
    {
        _arCameraManager.requestedFacingDirection = CameraFacingDirection.User;
    }
    
    /// <summary>
    /// Method ChangeToBack
    /// This method change to back camera
    /// </summary>
    public void ChangeToBack()
    {
        _arCameraManager.requestedFacingDirection = CameraFacingDirection.World;
    }
}
