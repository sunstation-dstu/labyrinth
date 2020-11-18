using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class connection : MonoBehaviour
{
    public enum typeFunction
    {
        nothing = 0,
        mediator = 1,
        light = 2
    }
    public typeFunction typeF;
    public GameObject connector;
    [HideInInspector]
    public int list;
    private int buffer;

    public List<mediator> med = new List<mediator>();

    void Start()
    {
        list = med.Count;
    }

    void Update()
    {
        if (typeF == typeFunction.light && connector.GetComponent<LeverArm>())
        {
            if (connector.GetComponent<LeverArm>().isActive) gameObject.transform.Find("Light").gameObject.SetActive(true);
            else gameObject.transform.Find("Light").gameObject.SetActive(false);
        }
        else if (typeF == typeFunction.light && connector.GetComponent<connection>())
        {
            if (connector.GetComponent<connection>().list > 0)
            {
                for (int i = 0; i < connector.GetComponent<connection>().list; i++)
                {
                    if (connector.GetComponent<connection>().med[i].connect) buffer += 1;
                }
                if (buffer == connector.GetComponent<connection>().list)
                {
                    gameObject.transform.Find("Light").gameObject.SetActive(true);
                    buffer = 0;
                }
                else
                {
                    gameObject.transform.Find("Light").gameObject.SetActive(false);
                    buffer = 0;
                }
            }
        }

            if (typeF == typeFunction.mediator && list > 0)
           { 
                for (int i = 0; i < list; i++)
                {
                    if (med[i].lever.GetComponent<LeverArm>().isActive) med[i].connect = true;
                    else med[i].connect = false;
                    if (med[i].connect) buffer += 1;
                }
                if (buffer == list)
                {
                    GetComponent<Animator>().SetBool("Active", true);
                    buffer = 0;
                }
                else
                {
                    GetComponent<Animator>().SetBool("Active", false);
                    buffer = 0;
                }
            }
       
    }
}

[Serializable]
public class mediator
{
    public bool connect;
    public GameObject lever;
}

