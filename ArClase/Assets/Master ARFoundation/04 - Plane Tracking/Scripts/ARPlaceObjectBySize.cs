using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaceObjectBySize : MonoBehaviour
{
    [SerializeField] private MeshRenderer _objectSize;
    [SerializeField] private GameObject _objectPrefab;

    private ARPlaneManager _planeManager;
    private List<ARPlane> _listPlanes;
    private GameObject _objectPlaced;

    private void Awake()
    {
        _planeManager = GetComponent<ARPlaneManager>();
        _listPlanes = new List<ARPlane>();
    }

    private void OnEnable()
    {
        _planeManager.planesChanged += PlanesFound;
    }

    private void OnDisable()
    {
        _planeManager.planesChanged -= PlanesFound;
    }

    private void PlanesFound(ARPlanesChangedEventArgs eventData)
    {
        if (eventData.added != null && eventData.added.Count > 0)
        {
            _listPlanes.AddRange(eventData.added);
        }

        foreach (var plane in _listPlanes)
        {
            if (CompareSizeWithObject(plane) && _objectPlaced == null)
            {
                _objectPlaced = Instantiate(_objectPrefab.gameObject);
                _objectPlaced.transform.position = plane.center;
                _objectPlaced.transform.up = plane.normal;

                StopPlaneDetection(plane);
            }
        }
    }

    private bool CompareSizeWithObject(ARPlane plane)
    {
        return plane.extents.x > _objectSize.bounds.size.x && plane.extents.y > _objectSize.bounds.size.z;
    }

    private void StopPlaneDetection(ARPlane planeException)
    {
        StopPlaneDetection();

        foreach (var plane in _listPlanes)
        {
            if (plane == planeException)
                plane.gameObject.SetActive(true);
        }
    }

    private void StopPlaneDetection()
    {
        _planeManager.requestedDetectionMode = PlaneDetectionMode.None;

        foreach (var plane in _listPlanes)
        {
            plane.gameObject.SetActive(false);
        }
    }

}
