using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform gunPoint;
    
    private GameObject activeGun;

    private GameObject ic;
    private GameObject othr;
    private GameObject gt;
    private cellSettings gt1;
    private cellSettings gt2;
    private UI inventoryUI;



    void Start()
    {
        ic = gameObject.transform.Find("Inventory Cell").gameObject;
        othr = ic.transform.Find("Other").gameObject;
        gt = ic.transform.Find("Gun & Tool").gameObject;
        gt1 = gt.transform.Find("1").gameObject.GetComponent<cellSettings>();
        gt2 = gt.transform.Find("2").gameObject.GetComponent<cellSettings>();
        inventoryUI = ic.GetComponent<UI>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            GameObject thisDropItem;
            if (gt1.iD != 0 && gt1.active)
            {
                activeGun.GetComponent<Rigidbody2D>().velocity = new Vector2(activeGun.transform.localScale.x, 2) * trowObj;
                
                gt1.iD = 0;
                activeGun.GetComponent<gun>().isActive = false;
            }
            if (gt2.iD != 0 && gt2.active)
            {
                thisDropItem = Instantiate(DropItem, new Vector3(player.transform.position.x, player.transform.position.y), quaternion.identity);
                thisDropItem.GetComponent<item>().id = gt2.iD;
                thisDropItem.GetComponent<SpriteRenderer>().sprite = itemlist[gt2.iD].icon;
                gt2.iD = 0;
                thisDropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(thisDropItem.transform.localScale.x, 1) * trowObj;
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            var allHit = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y + 1), Vector3.right * player.transform.localScale.x, distance);
            for (var i = 0; i < allHit.Length; i++)
            {
                if (allHit[i].collider.gameObject.layer == 11)
                {
                    var itItemObj = allHit[i].collider.gameObject;
                    var itItem = itItemObj.GetComponent<item>();
                    var isPickUp = false;
                    var isDestroy = true;
                    if (gt1.iD == 0 && itemlist[itItem.id].type == items.typeMove.gun)
                    {
                        gt1.iD = itItem.id;
                        activeGun = itItemObj;
                        activeGun.GetComponent<gun>().isActive = true;
                        isPickUp = true;
                        isDestroy = false;
                    }
                    else if (gt2.iD == 0 && itemlist[itItem.id].type == items.typeMove.tool)
                    {
                        gt2.iD = itItem.id;
                        isPickUp = true;
                    }
                    else if (itemlist[itItem.id].type == items.typeMove.ammo)
                    {
                        for (int j = 1; j <= inventoryUI.childCountOther; j++)
                        {
                            string number = j.ToString();
                            GameObject findCell = othr.transform.Find(number).gameObject;
                            cellSettings cell = findCell.GetComponent<cellSettings>();
                            if (cell.iD == 0)
                            {
                                cell.iD = itItem.id;
                                cell.filingStack+=itemlist[itItem.id].itemUp;
                                isPickUp = true;
                                break;
                            }
                            else if (cell.iD == itItem.id && itemlist[itItem.id].isStacked)
                            {
                                cell.filingStack+=itemlist[itItem.id].itemUp;
                                isPickUp = true;
                                break;
                            }
                        }
                    }
                    if (isPickUp && isDestroy)
                        Destroy(itItemObj);
                    else if (!isPickUp)
                    {
                        inventoryUI.noPlace();
                    }
                    break;
                }
                if (allHit[i].collider.gameObject.layer == 8) break;
            }
        }
        if(gt1.iD!=0) activeGun.transform.position = gunPoint.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // TODO null catch
        Gizmos.DrawLine(new Vector2(player.transform.position.x, player.transform.position.y + 1), new Vector2(player.transform.position.x, player.transform.position.y + 1) + Vector2.right * distance);
    }

    [Serializable]
    public class items
    {
        public Sprite icon;
        public enum typeMove
        {
            gun,
            tool,
            ammo
        }
        public typeMove type;
        public bool isStacked = false;
        public int itemUp;
    }

}
