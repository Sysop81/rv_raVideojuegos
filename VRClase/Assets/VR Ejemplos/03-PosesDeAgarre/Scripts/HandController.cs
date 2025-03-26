using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionReference controllerActionGrip;
    [SerializeField] private InputActionReference controllerActionTrigger;
    [SerializeField] private Animator handAnimator;
    
    private const string CLOSE = "Close";
    private const string INDEX = "Index";

    private void OnEnable()
    {
        controllerActionGrip.action.performed += GripActionPerformed;
        controllerActionGrip.action.canceled += GripActionCanceled;

        controllerActionTrigger.action.performed += TriggerActionPerformed;
        controllerActionTrigger.action.canceled += TriggerActionCanceled;
    }

    private void OnDisable()
    {
        controllerActionGrip.action.performed -= GripActionPerformed;
        controllerActionGrip.action.canceled -= GripActionCanceled;
        
        controllerActionTrigger.action.performed -= TriggerActionPerformed;
        controllerActionTrigger.action.canceled -= TriggerActionCanceled;
    }

    
    private void GripActionPerformed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(CLOSE, controllerActionGrip.action.ReadValue<float>());
    }

    private void GripActionCanceled(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(CLOSE, 0);
    }
    
    private void TriggerActionPerformed(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(INDEX, controllerActionTrigger.action.ReadValue<float>());
    }

    private void TriggerActionCanceled(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat(INDEX, 0);
    }
}
