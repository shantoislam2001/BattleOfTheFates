using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Joystick movementJoystick;
    public Button jumpButton;
    public Transform cameraTransform;  // Assign your camera here
    public Animator animator;  // Animator for handling animations

    public float walkSpeed = 2f;  // Speed for walking
    public float runSpeed = 5f;  // Speed for running
    public float turnSpeed = 10f;  // Controls player rotation speed
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public float cameraDistance = 5f;  // Distance of camera from player
    public float cameraSensitivity = 2f;  // Sensitivity for camera rotation
    public float cameraFollowSpeed = 5f;  // Speed at which the camera follows the player
    public float minCameraY = -30f;  // Minimum vertical angle for camera rotation
    public float maxCameraY = 60f;   // Maximum vertical angle for camera rotation

    private Vector3 moveDirection;
    private float verticalVelocity;
    private bool isGrounded;

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void Start()
    {
        // Add jump event listener
        jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        // Handle movement with joystick
        MoveCharacter();

        // Handle camera rotation with touch swipe
        HandleCameraRotation();

        // Apply gravity
        ApplyGravity();

        // Move the character controller
        controller.Move(moveDirection * Time.deltaTime);

        // Smoothly follow the player
        SmoothCameraFollow();
    }

    private void MoveCharacter()
    {
        // Get movement input from joystick
        float moveX = movementJoystick.Horizontal;
        float moveZ = movementJoystick.Vertical;

        // Calculate move direction (relative to camera's facing direction)
        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;

        // Calculate joystick magnitude to determine walk or run
        float joystickMagnitude = Mathf.Clamp01(new Vector2(moveX, moveZ).magnitude);

        // Determine movement speed based on joystick magnitude
        float targetSpeed = joystickMagnitude > 0.5f ? runSpeed : walkSpeed;

        // Update animations based on movement speed
        if (joystickMagnitude > 0.1f)
        {
            animator.SetBool("Walk", joystickMagnitude <= 0.5f);
            animator.SetBool("Run", joystickMagnitude > 0.5f);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }

        if (move.magnitude >= 0.1f)
        {
            // Player rotates to face the movement direction
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player forward with the calculated speed
            Vector3 moveDirectionForward = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDirection.x = moveDirectionForward.x * targetSpeed;
            moveDirection.z = moveDirectionForward.z * targetSpeed;
        }
        else
        {
            // Stop the movement when no input is detected
            moveDirection.x = 0f;
            moveDirection.z = 0f;
        }
    }

    private void HandleCameraRotation()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Only register touches on the right half of the screen
            if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    // Get swipe direction and rotate camera around player
                    float rotationX = touch.deltaPosition.x * cameraSensitivity * Time.deltaTime;
                    float rotationY = touch.deltaPosition.y * cameraSensitivity * Time.deltaTime;

                    // Adjust the camera's rotation based on input
                    currentRotationX += rotationX;
                    currentRotationY -= rotationY;

                    // Clamp the vertical camera rotation to avoid flipping
                    currentRotationY = Mathf.Clamp(currentRotationY, minCameraY, maxCameraY);
                }
            }
        }
    }

    private void SmoothCameraFollow()
    {
        // Calculate the camera's desired position based on player's position and current rotation
        Quaternion cameraRotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
        Vector3 cameraOffset = cameraRotation * new Vector3(0f, 1f, -cameraDistance);  // Offset behind player

        // Desired camera position
        Vector3 targetPosition = transform.position + cameraOffset;

        // Smoothly move the camera towards the target position
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);

        // Ensure the camera looks at the player
        cameraTransform.LookAt(transform.position + Vector3.up);
    }

    private void ApplyGravity()
    {
        // Check if the character is grounded
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Slight downward force to keep grounded
            animator.SetBool("Jump", false);  // Reset jumping animation when grounded
        }

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;
    }

    private bool jumping = true;

    void setValue()
    {
        jumping = true;
    }

    private void Jump()
    {

        if (jumping)
        {
            Debug.Log("Jumping");
            // Calculate jump velocity
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jump", true);  // Set jumping animation
            jumping = false;
            Invoke("setValue", 1f);
        }
    }
}
