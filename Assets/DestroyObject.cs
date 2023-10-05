using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private Transform vfx;

    public void DestroyActive()
    {
        //Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    
}
