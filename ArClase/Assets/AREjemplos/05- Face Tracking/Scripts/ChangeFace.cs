using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFace : MonoBehaviour
{
    [SerializeField] private Texture2D[] textures;
    [SerializeField] private bool isLoop;
    
    [Header("References")]
    [SerializeField] private NavigateArray navigateArray;
    [SerializeField] private Material material;
    
    // Start is called before the first frame update
    void Start()
    {
       navigateArray.Initialize(textures.Length,isLoop,OnChangeFace);
       OnChangeFace(0);
    }

    private void OnChangeFace(int index)
    {
        material.SetTexture("_BaseMap", textures[index]);
    }
}
