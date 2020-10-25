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

    /// <summary>
    /// Совпадает ли фактическое направление движения (нажатие на клавиатуре) с
    /// действительным (направление спрайта игрока)
    /// </summary>
    bool isSameDirections;

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
    private void Movement(MovementStatuses status)
    {
        switch (status)
        {
            case MovementStatuses.Idle: 
                anim.SetBool("isRunning", false);
                break;
            case MovementStatuses.Run:
            case MovementStatuses.Walk:
                if (isSameDirections)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }

                float movementSpeed = status.Equals(MovementStatuses.Walk) ? walkSpeed : runSpeed;
                transform.Translate(new Vector2(movementSpeed, 0) * Input.GetAxis("Horizontal") * Time.deltaTime);

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
        if (isOnGround)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                isSameDirections = Input.GetAxis("Horizontal") > 0 == spriteRenderer.flipX;
                Movement(MovementStatuses.Walk);
            }
            else Movement(MovementStatuses.Idle);

            if (Input.GetKeyDown(KeyCode.W)) Movement(MovementStatuses.Jump);
        }
    }
}
