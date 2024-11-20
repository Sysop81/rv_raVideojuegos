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
    
    private ARPlaneManager _planeManager;
    private List<ARPlane> _listPlanes;
    private GameObject _objectPlaced;
    
    /// <summary>
    /// Method Start [Life cycles]
    /// </summary>
    void Start()
    {
        _planeManager = GetComponent<ARPlaneManager>();
        _listPlanes = new List<ARPlane>();
    }
    
    /// <summary>
    /// Method OnEnable
    /// This method subscribes the property to method PlanesFound
    /// </summary>
    private void OnEnable()
    {
        if (_planeManager == null) // I think the problem is using start and not awake to initialize the _planeManager variable
            _planeManager = GetComponent<ARPlaneManager>();

        _planeManager.planesChanged += PlanesFound;
    }
    
    /// <summary>
    /// Method OnDisable
    /// This method unsubscribe the property to PlanesFound
    /// </summary>
    private void OnDisable()
    {
        _planeManager.planesChanged -= PlanesFound;
    }
    
    /// <summary>
    /// Method PlanesFound
    /// This method start plane detection with the objective of instantiating the prefab with the door to the secret room
    /// </summary>
    /// <param name="eventArgs"></param>
    private void PlanesFound(ARPlanesChangedEventArgs eventArgs)
    {
        if (eventArgs.added != null && eventArgs.added.Count > 0)
        {
            _listPlanes.AddRange((eventArgs.added));
        }

        foreach (var plane in _listPlanes)
        {
            if (CompareSizeWithObject(plane) && _objectPlaced == null)
            {
                _objectPlaced = Instantiate(objectPrefab.gameObject);
                _objectPlaced.transform.position = plane.center;
                _objectPlaced.transform.up = plane.normal;

                StopPlaneDetection(plane);
            }
        }
    }
    
    /// <summary>
    /// Method CompareSizeWithObject
    /// This method determines if the plane has the correct measurements to place the object [door]
    /// </summary>
    /// <param name="plane"></param>
    /// <returns></returns>
    private bool CompareSizeWithObject(ARPlane plane)
    {
        return plane.extents.x >  objectSize.bounds.size.x && 
               plane.extents.y > objectSize.bounds.size.z;
    }
    
    /// <summary>
    /// Method StopPlaneDetection
    /// This method ends the detection of planes
    /// </summary>
    /// <param name="planeException"></param>
    private void StopPlaneDetection(ARPlane planeException)
    {
        _planeManager.requestedDetectionMode = PlaneDetectionMode.None;

        foreach (var plane in _listPlanes)
        {
            plane.gameObject.SetActive(false);
            if(plane == planeException)
                plane.gameObject.SetActive(true);
        }
    }
}
