using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct ModelInfo
{
    public GameObject model;
    public string description;
    public bool isActivated;
}

public class UpdateModelInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleTXT;
    [SerializeField] private TextMeshProUGUI _descriptionTXT;

    public void ReceiveData(ModelInfo data)
    {
        _titleTXT.text = data.model.name;
        _descriptionTXT.text = data.description;
    }
}
