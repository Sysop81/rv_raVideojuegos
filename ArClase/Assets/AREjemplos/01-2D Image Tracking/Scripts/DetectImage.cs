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
    
    public void Awake()
    {
        _imageManager = GetComponent<ARTrackedImageManager>();
    }
    
    // Start is called before the first frame update
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

    private void OnEnable()
    {
        _imageManager.trackedImagesChanged += ImageFound;
    }

    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= ImageFound;
    }

    

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

    private void ChangeModelState(ARTrackedImage trackedImage, bool show)
    {
        GameObject currentModel = _imageDictionary[trackedImage.referenceImage.name].model;
        currentModel.transform.position = trackedImage.transform.position;

        if (show)
        {
            if (!isModelActivated(trackedImage))
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
            if (isModelActivated(trackedImage))
            {
                currentModel.SetActive(false);
                ModelInfo tempInfo = _imageDictionary[trackedImage.referenceImage.name];
                tempInfo.model = currentModel;
                tempInfo.isActivated = false;
                _imageDictionary[trackedImage.referenceImage.name] = tempInfo;
            }
        }
    }

    private bool isModelActivated(ARTrackedImage trackedImage)
    {
        return _imageDictionary[trackedImage.referenceImage.name].isActivated;
    }
}
