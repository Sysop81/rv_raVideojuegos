using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ARPortalCamera : MonoBehaviour
{
    private void Start()
    {
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = .1f;

        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
}
