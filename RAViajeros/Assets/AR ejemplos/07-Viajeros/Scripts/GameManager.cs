using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Stopwatch = System.Diagnostics.Stopwatch;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0; 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private ParticleSystem fireworksParticle;
    [SerializeField] private VPSManager vpsManager;
    private AudioSource _audioSource;
    private GameObject _object;
    private int _numOfObjects;
    private Stopwatch _stopwatch;
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        _stopwatch = new Stopwatch();
        _numOfObjects = vpsManager.GetSizeOfSpatialObject();
        _audioSource = Camera.main.GetComponent<AudioSource>();
        UpdateScore();
        _stopwatch.Start();
        InvokeRepeating("ManageTimer",0,1f);
    }
    
    /// <summary>
    /// Method ManageEndGame
    /// This method manages the end of game
    /// </summary>
    private void ManageEndGame()
    {
        // If the score is equal to count of prefab object list
        if (score == _numOfObjects)
        {
            // Set end game
            _stopwatch.Stop();
            gameOverPanel.gameObject.SetActive(true);
        }
    }
    
    /// <summary>
    /// Method ManageTimer
    /// This method get the elapsed time and update the timer text UI
    /// </summary>
    private void ManageTimer()
    {
        TimeSpan ts = _stopwatch.Elapsed;
        timerText.text = ts.ToString("mm\\:ss");
    }
    
    
    /// <summary>
    /// Method UpdateScore
    /// This method update the canvas panel label to show score info 
    /// </summary>
    private void UpdateScore()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }
    
    
    /// <summary>
    /// Method LaunchParticleSystem
    /// This method build a other object and launch the particle system corrurine
    /// </summary>
    /// <param name="touchedObject"></param>
    public void LaunchParticleSystem(Transform touchedObject)
    {
        _object = touchedObject.gameObject;
        fireworksParticle.gameObject.transform.position = touchedObject.position;
        StartCoroutine(ManageParticleSystem());
    }

    /// <summary>
    /// Method ManageParticleSystem [Corrutine]
    /// This corrutine manages the coin sound, particle system and finally change a bat position
    /// </summary>
    /// <returns></returns>
    private IEnumerator ManageParticleSystem()
    {
        score++;
        UpdateScore();
        Destroy(_object);
        _audioSource.Play();
        fireworksParticle.Play();
        yield return new WaitForSeconds(fireworksParticle.main.duration);
        // Manges the end game
        ManageEndGame();
    }
}
