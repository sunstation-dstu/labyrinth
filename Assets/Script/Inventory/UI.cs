using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject inv;
    [HideInInspector]
    public int childCountOther;
    GameObject other;
    GameObject gt; //Gun&Tool

    void Start()
    {
        gt = transform.Find("Gun & Tool").gameObject;
        other = transform.Find("Other").gameObject;
        childCountOther = other.transform.childCount;
    }

    void Update()
    {
        for (int i = 1; i <= childCountOther; i++)
        {
            string number = i.ToString();
            GameObject findCell = other.transform.Find(number).gameObject;
            int cellID = findCell.GetComponent<cellSettings>().iD;
            if (cellID != 0)
            {
                GameObject itemCell = findCell.transform.Find("Item").gameObject;
                itemCell.SetActive(true);
                itemCell.GetComponent<Image>().sprite = inv.GetComponent<Inv>().itemlist[cellID].icon;
            }
            else findCell.transform.Find("Item").gameObject.SetActive(false);
        }
        for(int i =1; i<=2; i++)
        {
            string number = i.ToString();
            GameObject findCell = gt.transform.Find(number).gameObject;
            int cellID = findCell.GetComponent<cellSettings>().iD;
            if (cellID != 0)
            {
                GameObject itemCell = findCell.transform.Find("Item").gameObject;
                itemCell.SetActive(true);
                itemCell.GetComponent<Image>().sprite = inv.GetComponent<Inv>().itemlist[cellID].icon;
            }
            else findCell.transform.Find("Item").gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            gt.transform.Find("1").gameObject.GetComponent<cellSettings>().active = true;
            gt.transform.Find("2").gameObject.GetComponent<cellSettings>().active = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            gt.transform.Find("2").gameObject.GetComponent<cellSettings>().active = true;
            gt.transform.Find("1").gameObject.GetComponent<cellSettings>().active = false;
        }
    }
}
