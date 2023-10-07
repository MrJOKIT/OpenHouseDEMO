using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSlide : MonoBehaviour
{
    
    [SerializeField] private float backgroundSpeed;
    [SerializeField] private float startTime;
    
    private float startTimeCounter;
    public bool start;
    

    public float BackgroundSpeed
    {
        get { return backgroundSpeed; }
        set { backgroundSpeed = value; }
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
            BackgroundSlider();
        }
        
    }

    

    private void BackgroundSlider()
    {
        transform.position += Vector3.right * backgroundSpeed * Time.deltaTime;
    }
}
