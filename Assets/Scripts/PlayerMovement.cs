using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerBoost boostScript;
    public InputAction MoveAction;
    public InputAction SprintAction;

    public float walkSpeed = 1f;
    public float sprintMultiplier = 1.5f;

    [HideInInspector]
    public float currentSpeedMultiplier = 1f; // Modified by PlayerBoost.cs

    public float turnSpeed = 20f;

    private Animator animator;
    private Rigidbody rb;

    private Vector3 movement;
    private Quaternion rotation = Quaternion.identity;

    public class PlayerTrigger3D : MonoBehaviour
{

}

    void OnEnable()
    {
        MoveAction.Enable();
        SprintAction.Enable();
    }

    void OnDisable()
    {
        MoveAction.Disable();
        SprintAction.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 input = MoveAction.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0, input.y);

        bool isWalking = movement.sqrMagnitude > 0.1f;
        animator.SetBool("IsWalking", isWalking);

        // Rotation
        if (isWalking)
        {
            Vector3 desiredForward = Vector3.RotateTowards(
                transform.forward,
                movement,
                turnSpeed * Time.deltaTime,
                0f
            );

            rotation = Quaternion.LookRotation(desiredForward);
            rb.MoveRotation(rotation);
        }

        // Determine speed
        float speed = walkSpeed;

       if (boostScript != null && boostScript.IsBoosting())
    {
        // Boost overrides sprint
     speed *= boostScript.boostMultiplier;
    }
        else if (SprintAction.IsPressed())
    {
        speed *= sprintMultiplier;
    }

        // Apply boost multiplier
        speed *= currentSpeedMultiplier;

        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }
}
