using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [Header("Frame Settings")]
    int MaxRate = 9999;
    public float TargetFrameRate = 60.0f;
    float currentFrameTime;
    
    [SerializeField] private TextMeshProUGUI fpsText;
    private float fpsCount;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxRate;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    }
    private void Update()
    {
        if (fpsText != null)
        {
            fpsCount = 1f / Time.deltaTime;
            fpsText.text = "FPS: " + Convert.ToInt32(fpsCount);
        }
    }
    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / TargetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }
}
