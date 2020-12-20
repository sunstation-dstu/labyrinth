using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Inv inventory;
    [HideInInspector]
    public int childCountOther;
    GameObject other;
    GameObject gt; //Gun&Tool
    [HideInInspector]
    public cellSettings gt1;
    private cellSettings gt2;
    public GameObject attentionText;
    private Text attentionTextEditor;
    private Animator attentionTextAnim;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inv>();
        gt = transform.Find("Gun & Tool").gameObject;
        gt1 = gt.transform.Find("1").gameObject.GetComponent<cellSettings>();
        gt2 = gt.transform.Find("2").gameObject.GetComponent<cellSettings>();
        other = transform.Find("Other").gameObject;
        childCountOther = other.transform.childCount;
        attentionTextEditor = attentionText.GetComponent<Text>();
        attentionTextAnim = attentionText.GetComponent<Animator>();
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
                itemCell.GetComponent<Image>().sprite = inventory.itemlist[cellID].icon;
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
                itemCell.GetComponent<Image>().sprite = inventory.itemlist[cellID].icon;
            }
            else findCell.transform.Find("Item").gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            gt1.active = true;
            gt2.active = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            gt2.active = true;
            gt1.active = false;
        }
    }

    public void noPlace()
    {
        attentionTextEditor.text = "Нет места!";
        attentionTextAnim.SetTrigger("attention");
    }

    public void reloading()
    {
        attentionTextAnim.SetBool("reload", true);
        attentionTextEditor.text = $"Перезарядка...";
    }

    public void stopR()
    {
        attentionTextAnim.SetBool("reload", false);
    }
}
