using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
[RequireComponent(typeof(ARTrackedImageManager))]
public class DetectImage : MonoBehaviour
{
    [SerializeField] private ModelInfo[] modelsInfo;
    public UnityEvent<ModelInfo> OnDetect;
    private ARTrackedImageManager _imageManager;
    private Dictionary<string, ModelInfo> _imageDictionary;
    
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
        _imageDictionary = new Dictionary<string, ModelInfo>();

        for (int i = 0; i < modelsInfo.Length; i++)
        {
            GameObject tempModel = Instantiate(modelsInfo[i].model, transform);
            tempModel.name = modelsInfo[i].model.name;
            tempModel.SetActive(false);

            ModelInfo tempInfo = modelsInfo[i];
            tempInfo.model = tempModel;
            tempInfo.isActivated = false;
            
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
            ChangeModelState(trackedImage, true);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            switch (trackedImage.trackingState)
            {
                case TrackingState.Tracking:
                    ChangeModelState(trackedImage, true);
                    break;
                case TrackingState.Limited:
                    ChangeModelState(trackedImage, false);
                    break;
                case TrackingState.None:
                    break;
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            ChangeModelState(trackedImage, false);
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
        GameObject currentModel = _imageDictionary[trackedImage.referenceImage.name].model;
        currentModel.transform.position = trackedImage.transform.position;

        if (show)
        {
            if (!IsModelActivated(trackedImage))
            {
                currentModel.SetActive(true);
                ModelInfo tempInfo = _imageDictionary[trackedImage.referenceImage.name];
                tempInfo.model = currentModel;
                tempInfo.isActivated = true;
                
                _imageDictionary[trackedImage.referenceImage.name] = tempInfo;
                OnDetect.Invoke(tempInfo); // call the handle
            }
        }
        else
        {
            if (IsModelActivated(trackedImage))
            {
                currentModel.SetActive(false);
                ModelInfo tempInfo = _imageDictionary[trackedImage.referenceImage.name];
                tempInfo.model = currentModel;
                tempInfo.isActivated = false;
                _imageDictionary[trackedImage.referenceImage.name] = tempInfo;
            }
        }
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
