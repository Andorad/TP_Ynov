using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    public List<Recipes> actualRecipeList;
    public List<GameObject> recipesToDestroy;

    [SerializeField] private List<Recipes> recipeList;

    [SerializeField] private GameObject recipe;
    [SerializeField] private GameObject recipes;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject finalPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Transform items;

    [SerializeField] private int score;

    [SerializeField] private Text scoreText;

    private bool canCreateRecipe;
    private bool gameIsPaused;

    private int recipeCount;

    private void Start()
    {
        canCreateRecipe = true;
        scoreText.text = "Score : " + score;
    }

    private void Update()
    {
        if(canCreateRecipe)
        {
            if(recipesToDestroy.Count <= 3)
            {
                StartCoroutine(WaitForNextRecipe());
                CreateNewRecipe(recipeList[Random.Range(0, recipeList.Count - 1)]);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void AddScore()
    {
        score += (int)recipesToDestroy[0].GetComponent<RecipeTimeController>().actualTimeLeft;
        scoreText.text = "Score : " + score;
        Destroy(recipesToDestroy[0]);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        finalPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void CreateNewRecipe(Recipes _Recipe)
    {
        actualRecipeList.Add(_Recipe);
        GameObject actualRecipe = Instantiate(recipe, recipes.transform.position, recipes.transform.rotation, recipes.transform);
        recipesToDestroy.Add(actualRecipe);
        actualRecipe.GetComponentInChildren<RawImage>().texture = _Recipe.visual;
        items = actualRecipe.transform.GetChild(1);

        for(int i = 0; i < _Recipe.items.Length; i++) 
        {
            GameObject actualItem = Instantiate(item, items.transform.position, items.transform.rotation, items.transform);
            actualItem.transform.Find("Visual").GetComponent<RawImage>().texture = _Recipe.items[i].visual;
            actualItem.GetComponentInChildren<Text>().text = _Recipe.items[i].name;
        }
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator WaitForNextRecipe()
    {
        canCreateRecipe = false;
        yield return new WaitForSeconds(10f);
        canCreateRecipe = true;
    }
}
