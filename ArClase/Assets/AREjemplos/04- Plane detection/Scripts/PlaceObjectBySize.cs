using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaceObjectBySize : MonoBehaviour
{
    [SerializeField] private MeshRenderer objectSize;
    [SerializeField] private GameObject objectPrefab;
    
    private ARPlaneManager _PlaneManager;
    private List<ARPlane> listPlanes;
    private GameObject objectPlaced;
    
    // Start is called before the first frame update
    void Start()
    {
        _PlaneManager = GetComponent<ARPlaneManager>();
        listPlanes = new List<ARPlane>();
    }

    private void OnEnable()
    {
        _PlaneManager.planesChanged += PlanesFound;
    }
    
    private void OnDisable()
    {
        _PlaneManager.planesChanged -= PlanesFound;
    }

    private void PlanesFound(ARPlanesChangedEventArgs eventArgs)
    {
        if (eventArgs.added != null && eventArgs.added.Count > 0)
        {
            listPlanes.AddRange((eventArgs.added));
        }

        foreach (var plane in listPlanes)
        {
            if (CompareSizeWithObject(plane) && objectPlaced == null)
            {
                objectPlaced = Instantiate(objectPrefab.gameObject);
                objectPlaced.transform.position = plane.center;
                objectPlaced.transform.up = plane.normal;

                StopPlaneDetection(plane);
            }
        }
    }

    private bool CompareSizeWithObject(ARPlane plane)
    {
        return plane.extents.x >  objectSize.bounds.size.x && 
               plane.extents.y > objectSize.bounds.size.z;
    }

    private void StopPlaneDetection(ARPlane planeException)
    {
        _PlaneManager.requestedDetectionMode = PlaneDetectionMode.None;

        foreach (var plane in listPlanes)
        {
            plane.gameObject.SetActive(false);
            if(plane == planeException)
                plane.gameObject.SetActive(true);
        }
    }
}
