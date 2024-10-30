using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.1f;
        
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
