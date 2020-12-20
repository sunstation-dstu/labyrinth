using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private const float stopJumpingDistance = 1f;

    [HideInInspector]
    public bool isRight;

    public Slider hpCount;
    private HPCount hp;
    
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
                break;
            case MovementStatuses.Jump:
                anim.SetTrigger(JumpTrigger);
                rigidBody.AddForce(new Vector2(0, jumpPower));
                break;
        }
    }
    
    public void Death()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    private void Start()
    {
        isRight = true;
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        hp = GetComponent<HPCount>();
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, GroundCheckingRadius, groundLayer);
    }

    private void Update()
    {
        hpCount.value = hp.hp;
        
        if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            Movement(MovementStatuses.Walk, Input.GetAxis("Horizontal"));
            anim.SetBool(IsWalking, true);
            anim.SetBool("IsRunning", false);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            Movement(MovementStatuses.Run, Input.GetAxis("Horizontal"));
            anim.SetBool(IsWalking, false);
            anim.SetBool("IsRunning", true);
        }
        else Movement(MovementStatuses.Idle);

        if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.W))
                Movement(MovementStatuses.Jump);
        }
        
        RaycastHit2D[] allHit = Physics2D.RaycastAll(transform.position, Vector3.down, stopJumpingDistance);
        for (int i = 0; i < allHit.Length; i++)
        {
            bool boofer = allHit[i].collider.gameObject.layer == 8;
            if (i == allHit.Length-1 && boofer)
                anim.SetBool("InFlight", false);
            else if (i == allHit.Length-1 && !boofer)
                anim.SetBool("InFlight", true);
            print(boofer);
        }
        // TODO
        Debug.DrawRay(transform.position,Vector3.down * stopJumpingDistance, Color.yellow);
    }
}
