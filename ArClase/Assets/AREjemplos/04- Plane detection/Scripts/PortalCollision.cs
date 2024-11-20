using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class PortalCollision : MonoBehaviour
{
    [SerializeField] private bool executionOnStart;
    public UnityEvent OnCollision;
    
    
    /// <summary>
    /// Method Start
    /// </summary>
    void Start()
    {
        if(executionOnStart)
            OnCollision.Invoke();
    }
    
    /// <summary>
    /// Method OnTriggerEnter
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        OnCollision.Invoke();
    }
}
