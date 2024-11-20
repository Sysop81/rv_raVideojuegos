using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneManager : MonoBehaviour
{
    
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject model3DPrefab;
    private List<ARPlane> planes = new List<ARPlane>();
    private GameObject modelEDPlaced;
    
    /// <summary>
    /// Method OnEnable [Handler]
    /// Subscribe the property to method PlaneFound
    /// </summary>
    void OnEnable()
    {
        arPlaneManager.planesChanged += PlaneFound;
    }

    /// <summary>
    /// Method OnDisable
    /// Unsubscribe the property to method PlaneFound
    /// </summary>
    void OnDisable()
    {
        arPlaneManager.planesChanged -= PlaneFound;
    }
    
    /// <summary>
    /// Method PlaneFound
    /// This method checks the detected plane to see if it is possible to place the cube prefab
    /// </summary>
    /// <param name="planeData"></param>
    private void PlaneFound(ARPlanesChangedEventArgs planeData)
    {
        if (planeData.added != null && planeData.added.Count > 0)
        {
            planes.AddRange(planeData.added);
        }

        foreach (var plane in planes)
        {
            if (plane.extents.x * plane.extents.y >= 0.2 && modelEDPlaced == null)
            {
                modelEDPlaced = Instantiate(model3DPrefab);
                float yOffset = modelEDPlaced.transform.localScale.y / 2;
                modelEDPlaced.transform.position = new Vector3(plane.center.x, plane.center.y + yOffset, plane.center.z);
                modelEDPlaced.transform.forward = plane.normal;
                StopPlaneDetection();
            }
        }
    }
    
    /// <summary>
    /// Method StopPlaneDetection
    /// This method stops the detection of planes
    /// </summary>
    private void StopPlaneDetection()
    {
        arPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
        foreach (var plane in planes)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
