using UnityEngine;

/// <summary>
/// Класс Игрока
/// </summary>
/// <remarks>
/// Класс включает в себя анимацию персонажа
/// </remarks>
public class Hero1 : MonoBehaviour
{
    /// <summary>
    /// TODO
    /// </summary>
    Rigidbody2D rigidBody;
    /// <summary>
    /// Скорость игрока
    /// </summary>
    private Animator anim;

    public float speedX;
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
    /// Радиус проверки нахождения игрока на земле
    /// </summary>
    const float groundCheckingRadius = 0.2f;
    /// <summary>
    /// Слой, который является "землёй"
    /// </summary>
    public LayerMask groundLayer;

    /// <summary>
    /// Перечисление состояний движения
    /// </summary>
    private enum MovementStatuses
    {
        Idle = 0,
        Backward = -1,
        Forward = 1
    }

    /// <summary>
    /// Предвижение игрока
    /// </summary>
    /// <param name="status">Текущий статус передвижения</param>
    private void Movement(MovementStatuses status)
    {
        if(status.Equals(MovementStatuses.Idle)){
            anim.SetBool("isRunning", false);
            return;
        }

        transform.Translate(new Vector2(status.Equals(MovementStatuses.Forward) ? speedX : -speedX, 0) * Time.deltaTime);

        if (status.Equals(MovementStatuses.Forward) ? gameObject.transform.localScale.x < 0 : gameObject.transform.localScale.x > 0)
        {
            // Для справки: здесь происходит умножение текущего вектора на дополнительный
            //              В данном случае на вектор направления движения - (x, y, z)
            gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(-1, 1, 1));
        }

        anim.SetBool("isRunning", true);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckingRadius, groundLayer);
    }

    void Update()
    {
        if (isOnGround)
        {
            if (Input.GetKey(KeyCode.D)) Movement(MovementStatuses.Forward);
            else if (Input.GetKey(KeyCode.A)) Movement(MovementStatuses.Backward);
            else Movement(MovementStatuses.Idle);

            if (Input.GetKeyDown(KeyCode.W))
            {
                rigidBody.AddForce(new Vector2(0, jumpPower));
            }
        }
    }
}
