using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int cash = 0;
    public Text check;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        check.text = "$" + cash;
    }
}
