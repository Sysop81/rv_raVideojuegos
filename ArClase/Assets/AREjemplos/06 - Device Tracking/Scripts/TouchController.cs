using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchController : MonoBehaviour
{
    public UnityEvent OnTouch;
    
    /// <summary>
    /// Method OnMouseDown [Event Listener]
    /// This method manage the touch on display screen
    /// </summary>
    private void OnMouseDown()
    {
        OnTouch.Invoke();
    }
}
