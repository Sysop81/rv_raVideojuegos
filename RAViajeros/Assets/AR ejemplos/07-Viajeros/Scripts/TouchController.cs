using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchController : MonoBehaviour
{
    public UnityEvent OnTouch;
    private GameManager _gameManager;
    
    /// <summary>
    /// Method Awake
    /// </summary>
    private void Awake()
    {
        // Get script component of the Game Manager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    /// <summary>
    /// Method Start
    /// </summary>
    private void Start()
    {
        OnTouch.AddListener(OnPrefabTouched);
    }
    
    
    /// <summary>
    /// Method OnMouseDown [Event Listener]
    /// This method manage the touch on display screen
    /// </summary>
    private void OnMouseDown()
    {
        OnTouch.Invoke();
    }
    
    /// <summary>
    /// Method OnPrefabTouched [Handler]
    /// This handler call the LaunchParticleSystem method in the Game Manager, to set a particle system explosion and destroy
    /// the object prefab.
    /// </summary>
    public void OnPrefabTouched()
    {
        if (_gameManager) _gameManager.LaunchParticleSystem(transform);
    }
}
