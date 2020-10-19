using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1 : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float speedX;
    public bool isGround = false; //проверка земли под ногами
    public Transform groundCheck;
    float groundRadius = 0.2f; //радиус проверки
    public LayerMask whatIsGround;
    public int jumpPower = 6000; //мощность прыжка

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(isGround)
            return;

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))//ходьба направо
        {
            transform.Translate(new Vector2(speedX, 0) * Time.deltaTime);
            if (gameObject.transform.localScale.x < 0) gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            anim.SetBool("isRunning", true); //анимация ходьбы
        } else if (Input.GetKey(KeyCode.A)) //ходьба налево
        {
            transform.Translate(new Vector2(-speedX, 0) * Time.deltaTime);
            if (gameObject.transform.localScale.x > 0) gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            anim.SetBool("isRunning", true); //анимация ходьбы
        } else anim.SetBool("isRunning", false);
        if (isGround && Input.GetKeyDown(KeyCode.W)) //прыжок
        {
            rigidBody.AddForce(new Vector2(0, jumpPower));
        }
    }
}
