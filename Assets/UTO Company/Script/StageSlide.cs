using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class StageSlide : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float startTime;
    public TextAnimator_TMP timeText;
    private float startTimeCounter;
    public bool start;
    bool three = true;
    bool two = true;
    bool one = true;

    private void Awake()
    {
        startPanel.SetActive(true);
    }

    public float SlideSpeed
    {
        get { return slideSpeed; }
        set { slideSpeed = value; }
    }

    private void Update()
    {
        startTimeCounter += Time.deltaTime;
        if (startTimeCounter >= startTime)
        {
            start = true;
        }
        TimeCounter();
    }

    private void TimeCounter()
    {
        
        if (startTimeCounter < 1 && three)
        {
            timeText.SetText("<shake>"+ 3);
            three = false;
        }
        else if (startTimeCounter < 2)
        {
            timeText.SetText("<shake>"+ 2);
            two = false;
        }
        else if (startTimeCounter < 3)
        {
            timeText.SetText("<shake>"+ 1);
            one = false;
        }
        else if (startTimeCounter < startTime && !start)
        {
            timeText.SetText("START");
        }
    }
    

    private void FixedUpdate()
    {
        if (start)
        {
            startPanel.SetActive(false);
            StageSlider();
        }
        
    }

    private void StageSlider()
    {
        transform.position += Vector3.left * slideSpeed * Time.deltaTime;
    }
}
