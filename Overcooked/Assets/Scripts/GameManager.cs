using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Recipes> recipeList;
    public List<Recipes> actualRecipeList;
    [SerializeField] private GameObject recipe;
    [SerializeField] private GameObject recipes;
    [SerializeField] private GameObject item;
    [SerializeField] private Transform items;
    [SerializeField] private int score;
    public List<GameObject> recipesToDestroy;
    private bool canCreateRecipe;
    private int recipeCount;

    private void Start()
    {
        canCreateRecipe = true;
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

    public void AddScore()
    {
        score += (int) recipesToDestroy[0].GetComponent<RecipeTimeController>().actualTimeLeft;
        Destroy(recipesToDestroy[0]);
    }

    IEnumerator WaitForNextRecipe()
    {
        canCreateRecipe = false;
        yield return new WaitForSeconds(10f);
        canCreateRecipe = true;
    }

}
