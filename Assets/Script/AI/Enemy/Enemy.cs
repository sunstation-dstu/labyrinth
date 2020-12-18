using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distance;
    public float movementSpeed;
    public int damage;

    private string targetTag = "Player";
    private int raysCount = 12;
    private float angle = 120;
    private Vector2 offset = new Vector2(0,3.5f);

    private float enemyDistance;
    private bool isRight = false;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool isActive;
    private bool onTrigger;
    private Animator anim;
    [HideInInspector]
    public bool attack;
    private bool stopIt;
    private HPCount hp;
    private Patrolling patrolling;
    
    private enum MovementStatuses
    {
        Idle = 0,
        Walk = 1,
        Fighting = 2,
        Patrol = 3
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = playerTransform.GetComponent<HPCount>();
        patrolling = GetComponent<Patrolling>();
    }
    

    private bool GetRaycast(Vector2 dir)
    {
        var result = false;
        var position = transform.position;
        var pos = new Vector2(position.x + offset.x, position.y + offset.y);
        var hits = Physics2D.RaycastAll(pos, dir, distance, LayerMask.GetMask("Player", "Ground"));

        foreach (var castHit in hits)
        {
            if (castHit.collider.gameObject.CompareTag(targetTag))
            {
                result = true;
                Debug.DrawLine(pos, castHit.point, Color.green);
                
            }
            else
            {
                Debug.DrawLine(pos, castHit.point, Color.red);
            }
            break;
        }

        return result;
    }

    private bool RayToScan()
    {
        var a = false;

        for (var rayIndex = 0; rayIndex < raysCount; rayIndex++)
        {
            var limitAngle = 90 - angle / 2;
            var rayAngle = (limitAngle + angle / (raysCount - 1) * rayIndex) * Mathf.Deg2Rad;
        
            var x = Mathf.Sin(rayAngle);
            var y = Mathf.Cos(rayAngle);
            
            var dir = transform.TransformDirection(new Vector2(-x * (transform.localScale.x/Mathf.Abs(transform.localScale.x)), y));
            Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), dir * distance, Color.gray);
            if (GetRaycast(dir)) a = true;
        }

        return a;
    }

    private void Movement(MovementStatuses status)
    {
        switch (status)
        {
            case MovementStatuses.Idle:
                anim.SetBool("Walk", false);
                anim.SetBool("Fighting", false);
                break;
            case MovementStatuses.Walk:
                anim.SetBool("Fighting", false);
                anim.SetBool("Walk", true);
                if(isRight)
                    rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
                else
                    rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
                break;
            case MovementStatuses.Fighting:
                anim.SetBool("Walk", false);
                anim.SetBool("Fighting", true);
                break;
            case MovementStatuses.Patrol:
                anim.SetBool("Walk", true);
                anim.SetBool("Fighting", false);
                if(isRight)
                    rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
                else
                    rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
                break;
        }
    }
    
    void Update()
    {
        enemyDistance = (transform.position - playerTransform.position).sqrMagnitude;
        if (Vector2.Distance(transform.position, playerTransform.position) < distance && hp.hp > 0)
        {
            var res = RayToScan();
            if (res)
            {
                isActive = true;
            }
        }

        if (enemyDistance > 500)
        {
            onTrigger = false;
            isActive = false;
            anim.SetBool("Fighting", false);
            anim.SetBool("Walk", false);
        }
        else
            onTrigger = true;

        if (isActive && onTrigger)
        {
            if (enemyDistance > 4f)
                Movement(MovementStatuses.Walk);
            else if (enemyDistance <= 4f && hp.hp > 0)
                Movement(MovementStatuses.Fighting);
            else
                Movement(MovementStatuses.Idle);
                

            if (transform.position.x - playerTransform.position.x < 0 == !isRight)
            {
                isRight = !isRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);
            }

            if (attack && stopIt)
            {
                hp.hp -= damage;
                attack = !attack;
                stopIt = false;
            }
            else if (!attack)
                stopIt = true;
        } else if (!isActive)
        {
            if (transform.position.x - patrolling.points[patrolling.currentPoint].position.x < 0 == !isRight)
            {
                isRight = !isRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);
            }
            Movement(MovementStatuses.Patrol);
        }

        if (hp.hp == 0)
            isActive = false;
    }
}
