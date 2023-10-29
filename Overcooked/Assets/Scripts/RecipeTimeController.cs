using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTimeController : MonoBehaviour
{
    [Header("References")]
    private GameManager gameManager;
    [Header("Variables")]
    [SerializeField] private Image timeLeftImage;
    [SerializeField] private float maxTimeLeft;
    [HideInInspector] public float actualTimeLeft;

    private void Start()
    {
        actualTimeLeft = maxTimeLeft;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        actualTimeLeft -= 1 * Time.deltaTime;
        timeLeftImage.fillAmount = actualTimeLeft / maxTimeLeft;

        if(actualTimeLeft <= 0)
        {
            gameManager.GameOver();
        }
    }
}
