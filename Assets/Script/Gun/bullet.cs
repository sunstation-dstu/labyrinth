using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public int bulletSpeed;
    public int damage = 20;
    private Player player;
    public int destroyDistance;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!player.isRight)
            bulletSpeed *= -1;
        rb.velocity = transform.right*bulletSpeed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
            Destroy(gameObject);
        if (other.gameObject.layer == 14)
        {
            other.gameObject.GetComponent<HPCount>().hp -= damage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if((gameObject.transform.position - player.transform.position).sqrMagnitude >destroyDistance) Destroy(gameObject);
    }
}
