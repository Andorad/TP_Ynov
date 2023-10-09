using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public int nbOfAsteroids;
    public static UIManager instance;
    [SerializeField]
    private Text asteroidLeftText;
    [SerializeField]
    private GameObject winText;


    void Awake()
    {
        instance = this;
        winText.SetActive(false);
    }


    void Update()
    {
        asteroidLeftText.text = "Asteroids left : " + nbOfAsteroids;

        if(nbOfAsteroids <= 0 )
        {
            winText.SetActive(true);
            StartCoroutine(GoToMenu());
        }
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
