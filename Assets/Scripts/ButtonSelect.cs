﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Button").GetComponent<Button>();

        button.Select();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
