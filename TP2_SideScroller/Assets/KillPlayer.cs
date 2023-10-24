using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject winText;
    [SerializeField]
    private GameObject loseText;
    [SerializeField]
    private GameObject toMainMenuButton;

    private void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        toMainMenuButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loseText.SetActive(true);
            toMainMenuButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
