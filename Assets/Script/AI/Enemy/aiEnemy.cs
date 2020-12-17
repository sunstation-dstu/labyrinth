using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiEnemy : MonoBehaviour
{
    public float distance;
    public float movementSpeed;

    private bool isRight = false;
    private Transform player;
    private sqrMagnitude sqrMagnitude;
    private Rigidbody2D rb;
    private bool isActive;
    private bool onTrigger;
    private Vector2 dir;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sqrMagnitude = GetComponent<sqrMagnitude>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dir = isRight ? Vector2.right : Vector2.left;
        var allHit = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 1), Vector2.left * transform.localScale.x, distance);
        for (var i = 0; i < allHit.Length; i++)
        {
            if (allHit[i].collider.gameObject.layer == 9)
            {
                isActive = true;
            }
        }

        if (sqrMagnitude.enemyDistance > 500)
        {
            onTrigger = false;
            isActive = false;
        }
        else
            onTrigger = true;
        if (isActive && onTrigger)
        {
            if(isRight && sqrMagnitude.enemyDistance > 4f)
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            else if (!isRight && sqrMagnitude.enemyDistance > 4f)
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            if (transform.position.x - player.position.x < 0 == !isRight)
            {
                isRight = !isRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // TODO null catch
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(transform.position.x, transform.position.y + 1) + dir * distance);
    }
}
