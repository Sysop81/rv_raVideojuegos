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
    [SerializeField] private ARTrackedImageManager _imageManager;
    private VideoPlayer _videoPlayer;
    private bool isImagegeTrackable;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        // Is image is tracked by _imageManager call to handler ImageFound
        _imageManager.trackedImagesChanged += ImageFound;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        // Disable app dessubcribe to handler _ImageFound
        _imageManager.trackedImagesChanged -= ImageFound;
    }
    
    // [Handler] Method ImageFoun -> params eventArgs
    private void ImageFound(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // First time
        foreach (var trackedImage in eventArgs.added)
        {
            _videoPlayer = trackedImage.GetComponentInChildren<VideoPlayer>();
            //_videoPlayer.SetTargetAudioSource(0, _videoPlayer.GetComponent<AudioSource>());
            _videoPlayer.Play();
        }
        // If target is Update
        foreach (var trackedImage in eventArgs.updated)
        {
            // Is target tracked
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!isImagegeTrackable)
                {
                    isImagegeTrackable = true;
                    _videoPlayer.gameObject.SetActive(true);
                    //_videoPlayer.SetTargetAudioSource(0, _videoPlayer.GetComponent<AudioSource>());
                    _videoPlayer.Play();
                }
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            { // Is target is not tracking "Not visible"
                if (isImagegeTrackable)
                {
                    isImagegeTrackable = false;
                    _videoPlayer.gameObject.SetActive(false);
                    _videoPlayer.Pause();
                    Debug.Log("PAUSE");
                }
            }
        }
    }
}
