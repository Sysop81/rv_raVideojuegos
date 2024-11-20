using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    /// <summary>
    /// Method Start [Life cycles]
    /// </summary>
    void Start()
    {
        // Set the radius of camera sphereCollider
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.1f;
        // Set the camera rb to kinematic
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
