using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Trigger OnCollisionEnter
    /// </summary>
    /// <param name="collision">Collision Object</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            audioSource.Play();
        }
    }
}
