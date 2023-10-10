using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private int[] scenesToLoad;

    [SerializeField]
    private Dropdown resolutionsDropdown;

    private void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> resolutionsOptions = new List<string>();
        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " ( " + resolutions[i].refreshRate + " Hz)";
            resolutionsOptions.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            } 
        }

        resolutionsDropdown.AddOptions(resolutionsOptions);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public void Play()
    {
        StartCoroutine(LoadScene(scenesToLoad[0]));
    }

    IEnumerator LoadScene(int sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
