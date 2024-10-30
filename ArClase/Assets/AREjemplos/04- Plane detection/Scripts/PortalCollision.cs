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
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(executionOnStart)
            OnCollision.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision.Invoke();
    }
}
