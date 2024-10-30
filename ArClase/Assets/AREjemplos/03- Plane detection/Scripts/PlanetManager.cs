using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanetManager : MonoBehaviour
{
    
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject model3DPrefab;
    private List<ARPlane> planes = new List<ARPlane>();
    private GameObject modelEDPlaced;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        arPlaneManager.planesChanged += PlanetFound;
    }

    // Update is called once per frame
    void OnDisable()
    {
        arPlaneManager.planesChanged -= PlanetFound;
    }

    private void PlanetFound(ARPlanesChangedEventArgs planetData)
    {
        if (planetData.added != null && planetData.added.Count > 0)
        {
            planes.AddRange(planetData.added);
        }

        foreach (var plane in planes)
        {
            if (plane.extents.x * plane.extents.y >= 0.2 && modelEDPlaced == null)
            {
                modelEDPlaced = Instantiate(model3DPrefab);
                float yOffset = modelEDPlaced.transform.localScale.y / 2;
                modelEDPlaced.transform.position = new Vector3(plane.center.x, plane.center.y + yOffset, plane.center.z);
                modelEDPlaced.transform.forward = plane.normal;
                StopPlanetDetection();
            }
        }
    }

    private void StopPlanetDetection()
    {
        arPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
        foreach (var plane in planes)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
