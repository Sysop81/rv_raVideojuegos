using System;
using System.Collections;
using System.Collections.Generic;
using Google.XR.ARCoreExtensions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class VPSManager : MonoBehaviour
{
    [Serializable]
    public struct  EarthPosition
    {
        public double Latitude;
        public double Longitude;
        public double Altitude;
    }

    [Serializable]
    public struct GeospatialObject
    {
        public GameObject objectPrefab;
        public EarthPosition earthPosition;
    }
    [SerializeField] private ARAnchorManager _arAnchorManager;
    [SerializeField] private AREarthManager _earthManager;
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private List<GeospatialObject> geospatialObjectsList = new List<GeospatialObject>();


    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        InvokeRepeating("VerifyGeospatialSupport",2f,1f);
    }
    
    /// <summary>
    /// Method VerifyGeospatialSupport
    /// This method verify the geospatial support 
    /// </summary>
    private void VerifyGeospatialSupport()
    {
        var result = _earthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);
        switch (result)
        {
            case FeatureSupported.Supported:
                Debug.Log("Peraparado para usar VPS");
                debugText.text = "Peraparado para usar VPS";
                CancelInvoke("VerifyGeospatialSupport");
                PlaceObjects();
                break;
            case FeatureSupported.Unknown:
                Debug.Log("Servicio desconocido");
                debugText.text = "Servicio desconocido";
                break;
            case FeatureSupported.Unsupported:
                Debug.Log("Servicio no soportado");
                debugText.text = "Servicio no soportado";
                break;
        }
        
    }
    
    /// <summary>
    /// Method PlaceObjects
    /// This method instantiate an object prefab in the geospatial position
    /// </summary>
    private void PlaceObjects()
    {
        debugText.text = "Situando objetos";
        if (_earthManager.EarthTrackingState == TrackingState.Tracking)
        {
            var geospatialPose = _earthManager.CameraGeospatialPose;

            foreach (var geospatialObject in geospatialObjectsList)
            {
                var earthPosition = geospatialObject.earthPosition;
                var objAnchor = ARAnchorManagerExtensions.AddAnchor(_arAnchorManager,
                    earthPosition.Latitude, earthPosition.Longitude, earthPosition.Altitude, Quaternion.identity);
                Instantiate(geospatialObject.objectPrefab, objAnchor.transform);
                debugText.text = "Instanciando "+ geospatialObject.objectPrefab.name + 
                                 "\n Lat: " + geospatialPose.Latitude + 
                                 "\n Lon: " + geospatialPose.Longitude + 
                                 "\n Alt: " + geospatialPose.Altitude;
            }
        }else if (_earthManager.EarthTrackingState == TrackingState.None)
        {
            Invoke("PlaceObjects",5.0f);
            debugText.text = "Invocando el método PlaceObjects";
        }
    }
    
    /// <summary>
    /// Getter GetSizeOfSpatialObject
    /// </summary>
    /// <returns>int Size of list</returns>
    public int GetSizeOfSpatialObject()
    {
        return geospatialObjectsList.Count;
    }
}
