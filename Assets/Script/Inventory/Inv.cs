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
    public GameObject DropItem;
    public GameObject player;
    [Header("Сила броска")]
    public float trowObj = 2f;
    private int isRight = 0;




    void Start()
    {
        
    }


    void Update()
    {
        GameObject ic = gameObject.transform.Find("Inventory Cell").gameObject;
        GameObject othr = ic.transform.Find("Other").gameObject;
        GameObject gt = ic.transform.Find("Gun & Tool").gameObject;
        cellSettings gt1 = gt.transform.Find("1").gameObject.GetComponent<cellSettings>();
        cellSettings gt2 = gt.transform.Find("2").gameObject.GetComponent<cellSettings>();

        if (player.GetComponent<SpriteRenderer>().flipX == false) isRight = 1;
        else if (player.GetComponent<SpriteRenderer>().flipX == true) isRight = -1;


        if (Input.GetKey(KeyCode.Q))
        {
            GameObject thisDropItem;
            if (gt1.iD != 0 && gt1.active)
            {
                thisDropItem = Instantiate(DropItem, new Vector3(player.transform.position.x, player.transform.position.y), quaternion.identity);
                thisDropItem.GetComponent<item>().id = gt1.iD;
                thisDropItem.GetComponent<SpriteRenderer>().sprite = itemlist[gt1.iD].icon;
                gt1.iD = 0;
                thisDropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(thisDropItem.transform.localScale.x * isRight, 1) * trowObj;
            }
            if (gt2.iD != 0 && gt2.active)
            {
                thisDropItem = Instantiate(DropItem, new Vector3(player.transform.position.x, player.transform.position.y), quaternion.identity);
                thisDropItem.GetComponent<item>().id = gt2.iD;
                thisDropItem.GetComponent<SpriteRenderer>().sprite = itemlist[gt2.iD].icon;
                gt2.iD = 0;
                thisDropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(thisDropItem.transform.localScale.x * isRight, 1) * trowObj;
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics2D.queriesStartInColliders = false;
            hit = Physics2D.Raycast(player.transform.position, Vector3.right * player.transform.localScale.x * isRight, distance);
            if (hit.collider != null && hit.collider.gameObject.layer == 11)
            {
                  GameObject itItemObj = hit.collider.gameObject;
                  item itItem = itItemObj.GetComponent<item>();
                  bool isPickUp = false;
                  if (gt1.iD == 0 && itemlist[itItem.id].type == items.typeMove.gun)
                  {
                      gt1.iD = itItem.id;
                      isPickUp = true;
                  }
                  else if (gt2.iD == 0 && itemlist[itItem.id].type == items.typeMove.tool)
                  {
                    gt2.iD = itItem.id;
                      isPickUp = true;
                  }
                  else if (itemlist[itItem.id].type == items.typeMove.other)
                  {
                      for (int i = 1; i <= ic.GetComponent<UI>().childCountOther; i++)
                      {
                        string number = i.ToString();
                        GameObject findCell = othr.transform.Find(number).gameObject;
                        int cellID = findCell.GetComponent<cellSettings>().iD;
                        if (cellID == 0)
                          {
                              findCell.GetComponent<cellSettings>().iD = itItem.id;
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
        public enum typeMove
        {
            gun,
            tool,
            other
        }
        public typeMove type;
        public bool isGun;
        public bool isInstrument;
    }

}
