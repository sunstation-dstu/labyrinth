using UnityEngine;

/// <summary>
/// Класс Игрока
/// </summary>
/// <remarks>
/// Класс включает в себя анимацию персонажа
/// </remarks>
public class Player : MonoBehaviour
{
    /// <summary>
    /// TODO
    /// </summary>
    Rigidbody2D rigidBody;

    private Animator anim;

    SpriteRenderer spriteRenderer;

    /// <summary>
    /// Скорость игрока при ходьбе
    /// </summary>
    public float walkSpeed;
    /// <summary>
    /// Скорость игрока при беге
    /// </summary>
    public float runSpeed;
    /// <summary>
    /// Мощность прыжка
    /// </summary>
    public int jumpPower = 6000;
    /// <summary>
    /// Находиться ли игрок на земле
    /// </summary>
    public bool isOnGround = false;
    /// <summary>
    /// Точка для проверки нахождения игрока на земле
    /// </summary>
    public Transform groundCheckPoint;
    /// <summary>
    /// Слой, который является "землёй"
    /// </summary>
    public LayerMask groundLayer;
    /// <summary>
    /// Радиус проверки нахождения игрока на земле
    /// </summary>
    const float groundCheckingRadius = 0.2f;

    public AudioSource Jump;
    public AudioSource WalkSound;

    /// <summary>
    /// Перечисление состояний движения
    /// </summary>
    public enum MovementStatuses
    {
        Jump = -1,
        Idle = 0,
        Walk = 1,
        Run = 2
    }

    /// <summary>
    /// Предвижение игрока
    /// </summary>
    /// <param name="status">Текущий статус передвижения</param>
    private void Movement(MovementStatuses status, float speedMultiplier = 1)
    {
        switch (status)
        {
            case MovementStatuses.Idle: 
                anim.SetBool("isRunning", false);
                WalkSound.Stop();
                break;
            case MovementStatuses.Run:
            case MovementStatuses.Walk:
                /// Совпадает ли фактическое направление движения (нажатие на клавиатуре) с
                /// действительным (направление спрайта игрока)
                if (Input.GetAxis("Horizontal") > 0 == spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    WalkSound.loop = true;
                    WalkSound.Play();
                }

                float movementSpeed = status.Equals(MovementStatuses.Walk) ? walkSpeed : runSpeed;
                rigidBody.velocity = new Vector2(speedMultiplier * movementSpeed, rigidBody.velocity.y);

                anim.SetBool("isRunning", true);
                break;
            case MovementStatuses.Jump:
                rigidBody.AddForce(new Vector2(0, jumpPower));
                break;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckingRadius, groundLayer);
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            Movement(MovementStatuses.Walk, Input.GetAxis("Horizontal"));
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            Movement(MovementStatuses.Run, Input.GetAxis("Horizontal"));
        } else Movement(MovementStatuses.Idle);

        if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rigidBody.AddForce(new Vector2(0, jumpPower));
                Jump.Play();
            }
        }
    }
}
