using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ARPortalCollision : MonoBehaviour
{
    [SerializeField] private bool _executeOnStart;

    public UnityEvent OnCollision;

    private void Start()
    {
        if (_executeOnStart)
            OnCollision.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision.Invoke();
    }

}
