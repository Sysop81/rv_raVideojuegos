using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavigateArray : MonoBehaviour
{

    [Header("References")] 
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Toggle toggle;

    public UnityAction<int> OnMove;
    
    private int currentIndex;
    private int arrayLength;
    private bool isLoop;
    
    private string _toggleOnText = "Activado";
    private string _toggleOffText = "Desactivado";
    private Text _toggleText;
    
    /// <summary>
    /// Method Initialize
    /// This method initialized all navigation UI controls
    /// </summary>
    /// <param name="arrayLength_"></param>
    /// <param name="isLoop_"></param>
    /// <param name="callback_"></param>
    public void Initialize(int arrayLength_, bool isLoop_, UnityAction<int> callback_)
    {
        currentIndex = 0;
        arrayLength = arrayLength_ - 1;
        isLoop = isLoop_;
        OnMove = callback_;
        
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
        // Manage the toggle action to activate // desactivate circular array navigation
        toggle.onValueChanged.AddListener(ManageButtonsArray);
        isLoop = toggle.isOn;
        _toggleText = toggle.GetComponentInChildren<Text>();
        _toggleText.text = _toggleOnText;

        CheckButtonState();
    }
    
    /// <summary>
    /// Method MoveLeft [Handler]
    /// This method manages the left UI button to decrement face mask
    /// </summary>
    private void MoveLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = isLoop ? arrayLength : 0;
        }
        
        CheckButtonState();
        OnMove.Invoke(currentIndex);
    }
    
    /// <summary>
    /// Method MoveRight [Handler]
    /// This method increment the right UI button to increment face mask
    /// </summary>
    private void MoveRight()
    {
        currentIndex++;
        if (currentIndex > arrayLength)
        {
            currentIndex = isLoop ? 0 : arrayLength;
        }
        
        CheckButtonState();
        OnMove.Invoke(currentIndex);
    }
    
    /// <summary>
    /// Method CheckButtonState
    /// This method manages limits when circular loop is disabled
    /// </summary>
    private void CheckButtonState()
    {
        if(!isLoop)
        {
            leftButton.interactable = currentIndex != 0;
            rightButton.interactable = currentIndex != arrayLength;
        }
    }
    
    /// <summary>
    /// Method ManageButtonsArray
    /// This method manages the loop variable. Activate or desactivate when user touch the toggle 
    /// </summary>
    /// <param name="value">bool Activate or desactivare</param>
    private void ManageButtonsArray(bool value)
    {
        isLoop = value;

        if (value)
        {
            leftButton.interactable = value;
            rightButton.interactable = value;
        }

        _toggleText.text = value ? _toggleOnText : _toggleOffText;
        CheckButtonState();
    }
}
