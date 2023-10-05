using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSlide : MonoBehaviour
{
    [SerializeField] private float slideSpeed;
    [SerializeField] private float startTime;
    private float startTimeCounter;
    public bool start;

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
    }

    private void FixedUpdate()
    {
        if (start)
        {
            StageSlider();
        }
        
    }

    private void StageSlider()
    {
        transform.position += Vector3.left * slideSpeed * Time.deltaTime;
    }
}
