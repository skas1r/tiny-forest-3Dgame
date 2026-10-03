using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseSpeed = 25f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private CinemachineCamera characterCamera;

    [Header("Ground")]
    [SerializeField] private float groundCheckDistance = 10f;
    [SerializeField] private float rayOffsetFromCenter = 7f;

    [SerializeField] private bool isGrounded;
    private float speed;
    private Vector3 moveVectorInput;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider col;

    public bool IsMoving => moveVectorInput.sqrMagnitude > 0.0001f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();

        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    public void AddMoveVectorInput(Vector3 moveVector)
    {
        moveVectorInput = moveVector;
    }

    public void AddRunningInput(bool runInput)
    {
        if (runInput && IsMoving)
        {
            speed = baseSpeed * 2f;
            animator?.SetFloat("velocity", 1.5f);
        }
        else
        {
            speed = baseSpeed;
        }
    }

    private void HandleMovement()
    {
        CheckForGround();

        // Якщо персонаж стоїть, то притискаємо його до землі, щоб він не зісковзував
        if (!IsMoving)
        {
            moveDirection = Vector3.zero;
            animator?.SetFloat("velocity", 0f);

            if (isGrounded)
            {
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
            }
            else
            {
                // Якщо персонаж не рухається і перебуває у повітрі,
                // повертаємо йому гравітацію для нормального падіння
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            return; // Не проводимо обчислення швидкості
        }

        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // Рух відносно камери
        moveDirection = characterCamera.transform.forward * moveVectorInput.z
                      + characterCamera.transform.right * moveVectorInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        float inputMagnitude = Mathf.Clamp01(moveVectorInput.magnitude);

        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(
                 moveDirection.x * speed * inputMagnitude,
                 rb.linearVelocity.y,
                 moveDirection.z * speed * inputMagnitude);
        }
        else
        {
            // У повітрі вертикаль віддаємо фізиці для падіння,
            // рух по горизонталі все ще враховуємо
            rb.useGravity = true;
            Vector3 velocity = moveDirection * speed * inputMagnitude;
            velocity.y = rb.linearVelocity.y;
            rb.linearVelocity = velocity;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        animator.SetFloat("velocity", inputMagnitude);
    }

    private void HandleRotation()
    {
        if (moveDirection.sqrMagnitude > 0f)
        {
            rotationDirection = moveDirection;
        }

        if (rotationDirection != Vector3.zero)
        {
            // Плавний поворот персонажа в сторону його руху
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            Quaternion rotation = Quaternion.Slerp(rb.rotation, targetRotation,
                rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rotation);
        }
    }
    private void CheckForGround()
    {
        Vector3 center = col.bounds.center;
        // Визначення математичного зміщення спереду та ззаду відносно напрямку погляду персонажа
        Vector3 forwardOffset = transform.forward * rayOffsetFromCenter;

        // Випускання двох променів чітко вниз
        bool isFrontGrounded = Physics.Raycast(center + forwardOffset, Vector3.down, groundCheckDistance);
        bool isBackGrounded = Physics.Raycast(center - forwardOffset, Vector3.down, groundCheckDistance);

        isGrounded = isFrontGrounded || isBackGrounded;
    }

    public void StopMovement()
    {
        // Зупинка руху персонажа
        moveVectorInput = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        animator?.SetFloat("velocity", 0f);
    }

}