using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvedMaterial : MonoBehaviour
{
    public Material material;
    [Range(0,1)]
    public float dissolveAmount;
    private List<Renderer> renderers = new List<Renderer>();
    
    // Костыль в виде рекурси
    private void ChildRendererSearch(Transform transform)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
                ChildRendererSearch(child);
        }
        else
        {
            var renderer = transform.GetComponent<Renderer>();
            if (renderer == null) return;
            
            Debug.Log(renderer);
            renderers.Add(renderer);
            renderer.material = new Material(material);
        }
    }
    
    private void Start()
    {
        ChildRendererSearch(transform);
    }
    
    void Update()
    {
        foreach (var renderer in renderers)
        {
            renderer.material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }
}
