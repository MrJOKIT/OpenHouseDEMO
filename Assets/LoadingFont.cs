using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingFont : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    
    private float loadTextCounter;

    private void Update()
    {
        loadTextCounter += Time.deltaTime;
        if (loadTextCounter < 0.5f)
        {
            loadingText.text = "Loading.";
        }
        else if (loadTextCounter < 1f)
        {
            loadingText.text = "Loading..";
        }
        else if (loadTextCounter < 1.5f)
        {
            loadingText.text = "Loading...";
        }
        else if (loadTextCounter < 2f)
        {
            loadingText.text = "Loading....";
        }
        else if (loadTextCounter < 2.5f)
        {
            loadingText.text = "Loading.....";
        }
        else if (loadTextCounter < 3f)
        {
            loadingText.text = "Loading......";
        }
        else if (loadTextCounter < 3.5f)
        {
            loadingText.text = "Loading.......";
        }
        else if (loadTextCounter < 4f)
        {
            loadingText.text = "Loading........";
        }
        else
        {
            loadTextCounter = 0f;
        }
    }
}
