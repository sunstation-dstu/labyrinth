using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.UI;

public class gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public int bulletID;
    public Transform muzzleCheckPoint;
    public Transform pointLeft;
    public Transform pointRight;
    private Player player;
    [HideInInspector]
    public bool isActive = false;

    public int reloadTime;

    public int patronSize;
    private int patronCount;

    private UI ui;
    private Text gunCellText;
    private GameObject other;

    private Collider2D gunCollider;
    private bool once;

    private Rigidbody2D rb;
    private bool isReloaded = true;
    private SpriteRenderer sr;

    private Transform armR;
    private Transform armL;

    public AudioSource RechargeSound;
    public AudioSource ShotSound;
    public AudioSource GunEmptySound;       // звуки оружия
    public AudioSource SelectionGunSound;
    public bool Actived = false;

    void Start()
    {
        armL = GameObject.FindGameObjectWithTag("LeftForearmLS").transform;
        armR = GameObject.FindGameObjectWithTag("RightForearmLS").transform;
        sr = GetComponent<SpriteRenderer>();
        gunCollider = gameObject.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        ui = GameObject.FindGameObjectWithTag("InventoryCell").GetComponent<UI>();
        other = GameObject.FindGameObjectWithTag("InventoryCell").transform.Find("Other").gameObject;
        gunCellText = GameObject.FindGameObjectWithTag("InventoryCell").gameObject.transform.Find("Gun & Tool").gameObject.transform.Find("1").gameObject.transform.Find("patronCounter").gameObject.GetComponent<Text>();
    }


    void Update()
    {
        if ((isActive)&&(Actived==false))
            { Actived = true; SelectionGunSound.Play(); } // звук подбора оружия
        if (!isActive) Actived = false;

        if (isActive && ui.gt1.active)
        {
            sr.enabled = true;
            Vector3 cursor;
            if (Input.GetKeyDown(KeyCode.Mouse0) && isReloaded)
                fire();
            if (Input.GetKeyDown(KeyCode.R) && isReloaded)
                reload();
            if(player.isRight)
                cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            else 
                cursor = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float rotateZ = Mathf.Atan2(cursor.y, cursor.x) * Mathf.Rad2Deg;

            if (isReloaded)
            {
                if (rotateZ < 90f && rotateZ > -90f) transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
                else if (rotateZ > 90f || rotateZ < -90f) transform.rotation = Quaternion.Euler(0f, 0f, 180 - rotateZ);
            } else if (player.isRight)
                transform.rotation = Quaternion.Euler(0f, 0f, -45);
            else if(!player.isRight)
                transform.rotation = Quaternion.Euler(0f, 0f, 45);

            if (!player.isRight == transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,transform.localScale.z);
            }
            armL.position = pointLeft.position;
            armR.position = pointRight.position;

            gunCellText.text = $"{patronCount}/{patronSize}";
        }
        else if(isActive && !ui.gt1.active)
        {
            sr.enabled = false;
        }
        else if (!isActive)
            gunCellText.text = "";

        if (once && isActive)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            gunCollider.enabled = false;
            once = false;
        }
        else if (!once && !isActive)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.rotation = Quaternion.identity;
            once = true;
            gunCollider.enabled = true;
        }
    }

    void fire()
    {
        if (patronCount > 0)
        {
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, muzzleCheckPoint.position, transform.rotation);
            patronCount--;
            ShotSound.Play();
        }
        else GunEmptySound.Play();
    }

    IEnumerator reloading(int reloadTime)
    {
        isReloaded = false;
        for (int i = 1; i <= ui.childCountOther; i++)
        {
            string number = i.ToString();
            GameObject findCell = other.transform.Find(number).gameObject;
            cellSettings cell = findCell.GetComponent<cellSettings>();
            if (cell.iD == bulletID && patronCount < patronSize)
            {
                ui.reloading();
                RechargeSound.Play();
                while (reloadTime > 0)
                {
                    yield return new WaitForSeconds(1);
                    if (reloadTime == 1)
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
                    reloadTime--;
                }
                break;
            }
        }
        ui.stopR();
        isReloaded = true;
        StopCoroutine(reloading(reloadTime));
    }

    void reload()
    {
        StartCoroutine(reloading(reloadTime));
    }
}
