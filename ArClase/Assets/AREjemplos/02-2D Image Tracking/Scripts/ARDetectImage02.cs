using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Security measure. Need this component to compile
[RequireComponent(typeof(ARTrackedImageManager))]

public class ARDetectImage02 : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;
    private VideoPlayer _videoPlayer;
    private bool _isImagegeTrackable;
    
    /// <summary>
    /// Method OnEnable [Handler]
    /// This method subscribes the property _imageManager to method ImageFound when the tracked image changes
    /// </summary>
    private void OnEnable()
    {
        // Is image is tracked by _imageManager call to handler ImageFound
        imageManager.trackedImagesChanged += ImageFound;
    }

    /// <summary>
    /// Method OnDisable [Handler]
    /// This method unsubscribes the property to method
    /// </summary>
    private void OnDisable()
    {
        // unsubscribes to handler _ImageFound
        imageManager.trackedImagesChanged -= ImageFound;
    }
    
    
    /// <summary>
    /// Method ImageFoun
    /// This method tracks the captured images and if they correspond to those established in the ReferenceIamgeLibrary,
    /// it plays the presentation video
    /// </summary>
    /// <param name="eventArgs"></param>
    private void ImageFound(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // First time
        foreach (var trackedImage in eventArgs.added)
        {
            _videoPlayer = trackedImage.GetComponentInChildren<VideoPlayer>();
            _videoPlayer.Play();
        }
        
        // If target is Update
        foreach (var trackedImage in eventArgs.updated)
        {
            // Is target tracked
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!_isImagegeTrackable)
                {
                    _isImagegeTrackable = true;
                    _videoPlayer.gameObject.SetActive(true);
                    _videoPlayer.Play();
                }
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            { // Is target is not tracking "Not visible"
                if (_isImagegeTrackable)
                {
                    _isImagegeTrackable = false;
                    _videoPlayer.gameObject.SetActive(false);
                    _videoPlayer.Pause();
                }
            }
        }
    }
}
