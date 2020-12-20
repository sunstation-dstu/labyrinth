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
    private Rigidbody2D rigidBody;
    private Animator anim;

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
    public bool isOnGround;
    /// <summary>
    /// Точка для проверки нахождения игрока на земле
    /// </summary>
    public Transform groundCheckPoint;
    /// <summary>
    /// Слой, который является "землёй"
    /// </summary>
    public LayerMask groundLayer;

    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int JumpTrigger = Animator.StringToHash("Jump");

    /// <summary>
    /// Радиус проверки нахождения игрока на земле
    /// </summary>
    private const float GroundCheckingRadius = 0.2f;

    [HideInInspector]
    public bool isRight;

    public AudioSource JumpSound; // звук прыжка

    /// <summary>
    /// Перечисление состояний движения
    /// </summary>
    private enum MovementStatuses
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
    /// <param name="speedMultiplier">Множитель скорости</param>
    private void Movement(MovementStatuses status, float speedMultiplier = 1)
    {
        switch (status)
        {
            case MovementStatuses.Idle:
                anim.SetBool(IsWalking, false);
                break;
            case MovementStatuses.Run:
            case MovementStatuses.Walk:
                // Совпадает ли фактическое направление движения (нажатие на клавиатуре) с
                // действительным (направление спрайта игрока)
                if (Input.GetAxis("Horizontal") < 0 == isRight)
                {
                    isRight = !isRight;
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }

                var movementSpeed = status.Equals(MovementStatuses.Walk) ? walkSpeed : runSpeed;
                rigidBody.velocity = new Vector2(speedMultiplier * movementSpeed, rigidBody.velocity.y);

                anim.SetBool(IsWalking, true);
                break;
            case MovementStatuses.Jump:
                anim.SetTrigger(JumpTrigger);
                rigidBody.AddForce(new Vector2(0, jumpPower));
                break;
        }
    }

    private void Start()
    {
        isRight = true;
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, GroundCheckingRadius, groundLayer);
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            Movement(MovementStatuses.Walk, Input.GetAxis("Horizontal"));
            anim.speed = 1.45f;
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            Movement(MovementStatuses.Run, Input.GetAxis("Horizontal"));
            anim.speed = 1.9f;
        }
        else Movement(MovementStatuses.Idle);

        if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            { Movement(MovementStatuses.Jump); JumpSound.Play(); } // звук прыжка
        }
        
        
    }
}
