﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public int bulletID;
    public Transform muzzleCheckPoint;
    private Player player;
    [HideInInspector]
    public bool isActive = false;

    public int patronSize;
    private int patronCount;

    private UI ui;
    private Text gunCellText;
    private GameObject other;

    private Collider2D collider;
    private bool once;

    private Rigidbody2D rb;

    void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ui = GameObject.FindGameObjectWithTag("InventoryCell").GetComponent<UI>();
        other = GameObject.FindGameObjectWithTag("InventoryCell").transform.Find("Other").gameObject;
        gunCellText = GameObject.FindGameObjectWithTag("InventoryCell").gameObject.transform.Find("Gun & Tool").gameObject.transform.Find("1").gameObject.transform.Find("patronCounter").gameObject.GetComponent<Text>();
    }


    void Update()
    {
        if (isActive)
        {
            Vector3 cursor;
            if (Input.GetKeyDown(KeyCode.Mouse0))
                fire();
            if (Input.GetKeyDown(KeyCode.R))
                reload();
            if(player.isRight)
                cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            else 
                cursor = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float rotateZ = Mathf.Atan2(cursor.y, cursor.x) * Mathf.Rad2Deg;
            
            if (rotateZ < 90f && rotateZ > -90f) transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
            else if (rotateZ > 90f || rotateZ < -90f) transform.rotation = Quaternion.Euler(0f, 0f, 180-rotateZ);

            if (!player.isRight == transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,transform.localScale.z);
            }

            gunCellText.text = $"{patronCount}/{patronSize}";
        }

        if (once && isActive)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            collider.enabled = false;
            once = false;
        }
        else if (!once && !isActive)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.rotation = Quaternion.identity;
            once = true;
            collider.enabled = true;
        }
    }

    void fire()
    {
        if (patronCount > 0)
        {
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, muzzleCheckPoint.position, transform.rotation);
            patronCount--;
        }
    }

    void reload()
    {
        for (int i = 1; i<=ui.childCountOther;i++)
        {
            string number = i.ToString();
            GameObject findCell = other.transform.Find(number).gameObject;
            cellSettings cell = findCell.GetComponent<cellSettings>();
            if (cell.iD == bulletID && patronCount < patronSize)
            {
                if (cell.filingStack >= (patronSize - patronCount))
                {
                    int boofer;
                    boofer = (patronSize - patronCount);
                    cell.filingStack -= boofer;
                    patronCount += boofer;
                }
                else
                {
                    patronCount += cell.filingStack;
                    cell.filingStack = 0;
                }
            }
        }
    }
}
