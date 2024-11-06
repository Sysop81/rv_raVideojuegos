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

    public UnityAction<int> OnMove;
    
    private int currentIndex;
    private int arrayLength;
    private bool isLoop;

    public void Initialize(int arrayLength_, bool isLoop_, UnityAction<int> callback_)
    {
        currentIndex = 0;
        arrayLength = arrayLength_ - 1;
        isLoop = isLoop_;
        OnMove = callback_;
        
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);

        CheckButtonState();
    }

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

    private void CheckButtonState()
    {
        if(!isLoop)
        {
            leftButton.interactable = currentIndex != 0;
            rightButton.interactable = currentIndex != arrayLength;
        }
    }
}
