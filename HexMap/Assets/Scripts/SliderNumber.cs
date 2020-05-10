﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNumber : MonoBehaviour
{
    [SerializeField]
    Text text;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
       slider = GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
       text.text =  slider.value.ToString();
    }
}
