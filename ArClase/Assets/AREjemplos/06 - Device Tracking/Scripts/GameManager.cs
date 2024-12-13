using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        UpdateScore();
    }
    
    /// <summary>
    /// Method AddScore
    /// This method update the game score varible
    /// </summary>
    public void AddScore()
    {
        score++;
        UpdateScore();
    }
    
    /// <summary>
    /// Method UpdateScore
    /// This method update the canvas panel label to show score info 
    /// </summary>
    private void UpdateScore()
    {
        scoreText.text = string.Format("Score: {0}", score);
    }
}
