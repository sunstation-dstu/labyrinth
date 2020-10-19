using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class Inv : MonoBehaviour
{
    [Header("Настройка луча")]
    public float distance = 2f;
    [HideInInspector]
    public RaycastHit2D hit;
    [Header("Список вещей")]
    public List<items> itemlist = new List<items>();
    [Header("Ячейки инвентаря")]
    public List<cell> celllist = new List<cell>();
    public GameObject DropItem;
    public GameObject player;
    [Header("Сила броска")]
    public float trowObj = 2f;
    private int isRight = 0;
    public enum TypeMove
    {
        Other,
        Weapon,
        Instrument
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.localScale.x > 0) isRight = 1;
        else if (player.transform.localScale.x < 0) isRight = -1;
        for (int i = 0; i < 5; i++)
        {
            if (celllist[i].iDItem != 0)
            {
                celllist[i].cellIcon.SetActive(true);
                celllist[i].cellIcon.GetComponent<Image>().sprite = itemlist[celllist[i].iDItem].icon;
            }
            else celllist[i].cellIcon.SetActive(false);
            if (celllist[i].isActive == true)
            {
                celllist[i].cellBG.SetActive(true);
            }
            else celllist[i].cellBG.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            celllist[0].isActive = true;
            celllist[1].isActive = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            celllist[0].isActive = false;
            celllist[1].isActive = true;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GameObject thisDropItem;
            if (celllist[0].iDItem != 0 && celllist[0].isActive)
            {
                thisDropItem = Instantiate(DropItem, new Vector3(player.transform.position.x, player.transform.position.y), quaternion.identity);
                thisDropItem.GetComponent<item>().id = celllist[0].iDItem;
                thisDropItem.GetComponent<SpriteRenderer>().sprite = itemlist[celllist[0].iDItem].icon;
                celllist[0].iDItem = 0;
                thisDropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(thisDropItem.transform.localScale.x * isRight, 1) * trowObj;
            }
            if (celllist[1].iDItem != 0 && celllist[1].isActive)
            {
                thisDropItem = Instantiate(DropItem, new Vector3(player.transform.position.x, player.transform.position.y), quaternion.identity);
                thisDropItem.GetComponent<item>().id = celllist[1].iDItem;
                thisDropItem.GetComponent<SpriteRenderer>().sprite = itemlist[celllist[1].iDItem].icon;
                celllist[1].iDItem = 0;
                thisDropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(thisDropItem.transform.localScale.x * isRight, 1) * trowObj;
            }
        }

        /* if (Input.GetKey(KeyCode.Q) && celllist[0].iDItem != 0)
        {
            GameObject thisDropItem;
            thisDropItem = Instantiate(DropItem, new Vector3(player.transform.position.x, player.transform.position.y), quaternion.identity);
            thisDropItem.GetComponent<item>().id = celllist[0].iDItem;
            thisDropItem.GetComponent<SpriteRenderer>().sprite = itemlist[celllist[0].iDItem].icon;
            celllist[0].iDItem = 0;
            thisDropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(thisDropItem.transform.localScale.x*isRight, 1) * trowObj;
        }*/


        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics2D.queriesStartInColliders = false;
            hit = Physics2D.Raycast(player.transform.position, Vector3.right * player.transform.localScale.x, distance);
            if (hit.collider != null && hit.collider.gameObject.layer == 11)
            {
                GameObject itItemObj = hit.collider.gameObject;
                  item itItem = itItemObj.GetComponent<item>();
                  bool isPickUp = false;
                  if (celllist[0].iDItem == 0 && itemlist[itItem.id].isGun)
                  {
                      celllist[0].iDItem = itItem.id;
                      isPickUp = true;
                  }
                  if (celllist[1].iDItem == 0 && itemlist[itItem.id].isInstrument)
                  {
                      celllist[1].iDItem = itItem.id;
                      isPickUp = true;
                  }
                  if (itemlist[itItem.id].isStacked && !itemlist[itItem.id].isGun && !itemlist[itItem.id].isInstrument)
                  {
                      for (int i = 2; i < 8; i++)
                      {
                          if (celllist[i].iDItem == itItem.id)
                          {
                              celllist[i].stack++;
                              isPickUp = true;
                              break;
                          }
                      }
                  }
                  else if (!itemlist[itItem.id].isStacked && !itemlist[itItem.id].isGun && !itemlist[itItem.id].isInstrument)
                  {
                      for (int i = 2; i < 8; i++)
                      {
                          if (celllist[i].iDItem == 0)
                          {
                              celllist[i].iDItem = itItem.id;
                              isPickUp = true;
                              break;
                          }
                      }
                  }
                if (isPickUp == true)
                    Destroy(itItemObj);
                else print("Нет места");
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.transform.position, player.transform.position + Vector3.right * isRight * distance);
    }

    [Serializable]
    public class items
    {
        public Sprite icon;
        public bool isStacked;
        public int valueStack;
        public bool isGun;
        public bool isInstrument;
        public TypeMove typeMove;
    }

    [Serializable]
    public class cell
    {
        public bool isActive;
        public GameObject cellFront;
        public GameObject cellIcon;
        public GameObject cellBG;
        public int iDItem;
        public int stack;
    }
}
