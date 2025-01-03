using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public struct ModelInfo2
{
    public GameObject model;
    public double currentPlayTime;
    public bool isActivated;
    public float yOffset;
}

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARDetectMultipleImages : MonoBehaviour
{
    
    [SerializeField] private ModelInfo2[] modelsInfo;
    [SerializeField] private TextMeshProUGUI debugText;
    private ARTrackedImageManager _imageManager;
    private Dictionary<string, ModelInfo2> _imageDictionary;
    private VideoPlayer _videoPlayer;
    private readonly Vector3 _planeLocalScale = new Vector3(0.068f, 0.1f, 0.07f);
    
    /// <summary>
    /// Method Awake [Life cycles]
    /// </summary>
    public void Awake()
    {
        _imageManager = GetComponent<ARTrackedImageManager>();
    }
    
    /// <summary>
    /// Method Start [Life cycle]
    /// </summary>
    void Start()
    {
        // Instantiate the dictionary
        _imageDictionary = new Dictionary<string, ModelInfo2>();
        
        // Loop array models to build the dictionary
        for (int i = 0; i < modelsInfo.Length; i++)
        {
            GameObject tempModel = Instantiate(modelsInfo[i].model,transform);
            tempModel.name = modelsInfo[i].model.name;
            tempModel.SetActive(false);

            ModelInfo2 tempInfo = modelsInfo[i];
            tempInfo.model = tempModel;
            
            _imageDictionary.Add(tempModel.name, tempInfo);
        }
    }
    
    /// <summary>
    /// Method OnEnable
    /// This method subscribes the property to method ImageFound
    /// </summary>
    private void OnEnable()
    {
        _imageManager.trackedImagesChanged += ImageFound;
    }
    
    /// <summary>
    /// Method OnDisable
    /// This method unsubscribes the property to ImageFound
    /// </summary>
    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= ImageFound;
    }
    
    /// <summary>
    /// Method ImageFound
    /// This method checks the tracked images to see if they correspond to those established in the library to display
    /// the 3D model of the planet and its information on the canvas.
    /// </summary>
    /// <param name="eventArgs"></param>
    private void ImageFound(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            //debugText.SetText("<color=green><b> ADDED event</b></color>");
            ChangeModelState(trackedImage, true);
            return;
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            switch (trackedImage.trackingState)
            {
                case TrackingState.Tracking:
                    //debugText.SetText("<color=blue><b> UPDATED-TRACKING-TRUE</b></color>");
                    ChangeModelState(trackedImage, true);
                    break;
                case TrackingState.Limited:
                    //debugText.SetText("<color=orange><b> UPDATED-LIMITED-FALSE</b></color>");
                    ChangeModelState(trackedImage, false);
                    break;
                case TrackingState.None:
                    //debugText.SetText("<color=red><b> UPDATED-NONE</b></color>");
                    ChangeModelState(trackedImage, false);
                    break;
            }
        }
    }
    
    /// <summary>
    /// Method ChangeModelState
    /// This method activates or desactivates the 3d planet and GUI information.
    /// </summary>
    /// <param name="trackedImage"></param>
    /// <param name="show"></param>
    private void ChangeModelState(ARTrackedImage trackedImage, bool show)
    {
        // Getting the current prefab and y axes value
        GameObject currentModel = _imageDictionary[trackedImage.referenceImage.name].model;
        var yOffset = _imageDictionary[trackedImage.referenceImage.name].yOffset;
        
        // Apply transforms values to prefab
        currentModel.transform.position = new Vector3(trackedImage.transform.position.x,
            trackedImage.transform.position.y + yOffset , trackedImage.transform.position.z);
        currentModel.transform.rotation = trackedImage.transform.rotation;
        currentModel.transform.localScale = _planeLocalScale; 
        
        // Get videoPlayer component && get the tempInfo model Struct
        _videoPlayer = currentModel.GetComponentInChildren<VideoPlayer>();
        var tempInfo = ManageModelInfo(trackedImage, currentModel, show);

        if (show)
        {
            if (!IsModelActivated(trackedImage))
            {
                currentModel.SetActive(true);
                _imageDictionary[trackedImage.referenceImage.name] = tempInfo;

                _videoPlayer.time = tempInfo.currentPlayTime;
                _videoPlayer.Play();
            }
        }
        else
        {
            if (IsModelActivated(trackedImage))
            {
                tempInfo.currentPlayTime = _videoPlayer.time;
                _imageDictionary[trackedImage.referenceImage.name] = tempInfo;

                _videoPlayer.Pause();
                currentModel.SetActive(false);
            }
        }
    }
    
    /// <summary>
    /// Method ManageModelInfo
    /// This method manages and get the current model from imageDictionary
    /// </summary>
    /// <param name="trackedImage"></param>
    /// <param name="currentModel"></param>
    /// <param name="isActivate"></param>
    /// <returns></returns>
    private ModelInfo2 ManageModelInfo(ARTrackedImage trackedImage,GameObject currentModel,bool isActivate)
    {
        ModelInfo2 tempInfo = _imageDictionary[trackedImage.referenceImage.name];
        tempInfo.model = currentModel;
        tempInfo.isActivated = isActivate;
        return tempInfo;
    }

    /// <summary>
    /// Method IsModelActivated
    /// This method determines is the current model is activated or not
    /// </summary>
    /// <param name="trackedImage"></param>
    /// <returns></returns>
    private bool IsModelActivated(ARTrackedImage trackedImage)
    {
        return _imageDictionary[trackedImage.referenceImage.name].isActivated;
    }
}
