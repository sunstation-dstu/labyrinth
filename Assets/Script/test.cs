using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.material = new Material(renderer.material);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
