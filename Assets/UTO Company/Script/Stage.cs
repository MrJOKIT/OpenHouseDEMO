using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private GameObject stageSlide;
    private void OnEnable()
    {
        stageSlide = GameObject.FindGameObjectWithTag("StageSlide").gameObject;
        transform.SetParent(stageSlide.transform);
    }
}
