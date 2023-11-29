using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader  : MonoBehaviour
{
    [Header("Menu Screens")] 
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    

    [Header("Slider")] [SerializeField] private Slider loadingSlider;
    
    public void LoadLevelBtn(string levelToLoad)
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        float time;
        if (levelToLoad == "PlayStage")
        {
            time = 4.5f;
        }
        else
        {
            time = 1.5f;
        }
        yield return new WaitForSeconds(time);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        /*while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }*/

        
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
