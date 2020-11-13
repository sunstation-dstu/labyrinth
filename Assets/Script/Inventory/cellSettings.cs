using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cellSettings : MonoBehaviour
{
    [HideInInspector]
    public bool active = false;
    public int iD;
    public enum typeItem
    {
        gun = 0,
        tool = 1,
        other = 2
    }
    public typeItem type;

    void Start()
    {
        
    }

    void Update()
    {
        if (active == true) gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
        else gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
    }
}
