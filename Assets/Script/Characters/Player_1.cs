using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public float speed;
    private Vector2 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        // rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
    }
}
