using UnityEngine;
using UnityEngine.InputSystem;

public class TPPControllerW : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10.0f; // Smooth character rotation speed

    [Header("Camera Settings")]
    public Transform cameraRig; // Reference to the CameraRig object
    public Transform mainCameraTransform; // Reference to the actual Camera (Main Camera)
    public float lookSensitivity = 1.0f;
    public float cameraSmoothTime = 0.1f;
    public Vector3 cameraOffset = new Vector3(0, 2, -4);

    private CharacterController controller;
    private Vector2 movementInput;
    private Vector2 lookInput;
    private bool isRunning;
    private Vector3 velocity;

    private Vector3 cameraVelocity; // For SmoothDamp
    private float verticalLookRotation = 0f; // To clamp vertical camera rotation
    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        ApplyGravity();
    }

    void LateUpdate()
    {
        SmoothCameraFollow();
        RotateCamera();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private bool jumping = true;
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && jumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumping = false;
            animator.SetBool("Jump", true);
            Invoke("resetValue", 1f);
        }
    }

    void resetValue()
    {
        jumping = true;
        animator.SetBool("Jump", false);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.performed;
    }

    private void Move()
    {
        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        if (direction.magnitude >= 0.1f)
        {
            // Determine target angle based on camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraRig.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Smoothly rotate the character to face the movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move character in the direction they’re facing
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * targetSpeed * Time.deltaTime);
           
            if(targetSpeed == 2)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
            else if(targetSpeed == 5)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
            } 
        }else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotateCamera()
    {
        // Horizontal rotation for the CameraRig (around the player)
        cameraRig.Rotate(Vector3.up * lookInput.x * lookSensitivity, Space.World);

        // Vertical rotation for the main camera (up/down, clamped)
        verticalLookRotation -= lookInput.y * lookSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -45f, 45f); // Adjust clamp range if needed
        mainCameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0, 0);
    }

    private void SmoothCameraFollow()
    {
        // Keep the cameraRig in the center of the player’s position
        cameraRig.position = transform.position;

        // Apply offset to the main camera based on cameraRig's rotation
        mainCameraTransform.localPosition = cameraOffset;
    }
}
