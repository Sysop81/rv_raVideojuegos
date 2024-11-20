using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]

// ModelInfo -> Struct object to map title and description planet properties in App canvas GUI
public struct ModelInfo
{
    public GameObject model;
    public string description;
    public bool isActivated;
}

public class UpdateModelInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    
    /// <summary>
    /// Method ReceiveData
    /// This method set the received data in all Text Mesh Pro GUI to update the planet info
    /// </summary>
    /// <param name="data">Planet data info</param>
    public void ReceiveData(ModelInfo data)
    {
        titleTxt.text = data.model.name;
        descriptionTxt.text = data.description;
    }
}
