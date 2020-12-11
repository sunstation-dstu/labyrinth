using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cellSettings : MonoBehaviour
{
    [HideInInspector]
    public bool active = false;
    public int iD;
    public int filingStack;
    private Text ammoCounter;
    private Text patronCounter;
    public enum typeItem
    {
        gun = 0,
        tool = 1,
        other = 2
    }
    public typeItem type;

    void Start()
    {
        ammoCounter = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (filingStack > 0 && type == typeItem.other)
            ammoCounter.text = $"x{filingStack}";
        else if (type == typeItem.other)
            ammoCounter.text = "";
        if (active == true) gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
        else gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
        if (filingStack == 0) iD = 0;
    }
}
