using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionReference controllerActionGrip;
    [SerializeField] private Animator handAnimator;
    
    private const string CLOSE = "Close";

    private void OnEnable()
    {
        controllerActionGrip.action.performed += GripActionPerformed;
        controllerActionGrip.action.canceled += GripActionCanceled;
    }

    private void OnDisable()
    {
        controllerActionGrip.action.performed -= GripActionPerformed;
        controllerActionGrip.action.canceled -= GripActionCanceled;
    }

    
    private void GripActionPerformed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(CLOSE, controllerActionGrip.action.ReadValue<float>());
    }

    private void GripActionCanceled(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(CLOSE, 0);
    }
}
