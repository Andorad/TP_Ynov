using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    
    private int score;

    private void Start()
    {
        AddPoints(0);
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = $"Score : {score}";
    }
}
