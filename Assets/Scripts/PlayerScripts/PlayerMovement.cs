using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private CinemachineCamera characterCamera;

    [Header("Ground")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private float maxSlopeAngle = 70f;

    private Vector3 moveVectorInput;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;
    private Vector3 groundNormal = Vector3.up;
    private Animator animator;
    private Rigidbody rb;
    private BoxCollider box;
    private bool isGrounded;

    public bool IsMoving => moveVectorInput.sqrMagnitude > 0.0001f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        animator = GetComponentInChildren<Animator>();
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
    private void HandleMovement()
    {
        CheckForGround();
        // Рухатися можна лише перебуваючи на землі з кутом нахилу не більшим за максимально дозволений
        float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);
        bool onWalkableGround = isGrounded && slopeAngle <= maxSlopeAngle;

        // Якщо персонаж стоїть, то притискаємо його до землі, щоб він не зісковзував
        if (!IsMoving)
        {
            moveDirection = Vector3.zero;
            animator.SetFloat("velocity", 0f);

            if (onWalkableGround)
            {
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
            }
            else
            {
                // Якщо персонаж не рухається і перебуває у повітрі
                // або на надто крутому схилі, повертаємо йому
                // гравітацію для нормального падіння
                rb.useGravity = true;
            }
            return; // Не проводимо обчислення швидкості
        }

        // Рух відносно камери
        moveDirection = characterCamera.transform.forward * moveVectorInput.z
                      + characterCamera.transform.right * moveVectorInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        float inputMagnitude = Mathf.Clamp01(moveVectorInput.magnitude);

        if (onWalkableGround)
        {
            // Рух уздовж поверхні 
            Vector3 slopeDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;
            rb.useGravity = false;
            rb.linearVelocity = slopeDirection * speed * inputMagnitude;
        }
        else
        {
            // У повітрі вертикаль віддаємо фізиці для падіння,
            // рух по горизонталі все ще враховуємо
            rb.useGravity = true;
            Vector3 velocity = moveDirection * speed * inputMagnitude;
            velocity.y = rb.linearVelocity.y;
            rb.linearVelocity = velocity;
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
        // Перевірка перебування на землі
        const float skin = 0.05f;

        Vector3 half = Vector3.Scale(box.size, transform.lossyScale) * 0.5f;
        Vector3 bottom = transform.TransformPoint(box.center) - Vector3.up * half.y;
        Vector3 origin = bottom + Vector3.up * (skin * 2f);
        Vector3 halfExtents = new Vector3(half.x * 0.95f, skin, half.z * 0.95f);

        isGrounded = Physics.BoxCast(origin, halfExtents, Vector3.down, out RaycastHit hit,
            transform.rotation, skin + groundCheckDistance, groundMask,
            QueryTriggerInteraction.Ignore);

        groundNormal = isGrounded ? hit.normal : Vector3.up;
    }

    public void StopMovement()
    {
        // Зупинка руху персонажа
        moveVectorInput = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        animator.SetFloat("velocity", 0f);
    }
}