using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class sqrMagnitude : MonoBehaviour
{
    [HideInInspector]
    public float enemyDistance;
    public float maxDistance = 2000;
    private Transform player;
    private aiEnemy ai;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ai = GetComponent<aiEnemy>();
    }
    
    void Update()
    {
        enemyDistance = (transform.position - player.position).sqrMagnitude;
        //print(enemyDistance);
        if (enemyDistance > maxDistance)
            ai.enabled = false;
        else
            ai.enabled = true;
    }

}
