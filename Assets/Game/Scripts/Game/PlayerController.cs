using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform model;
    private bool _allowMove = true;
    private string _currentAnim = "idle";
    
    private Vector3 velocity;

    [Header("Movement")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look")]
    [SerializeField] private Joystick joystickRotation;
    [SerializeField] private float lookSensitivity = 120f;
    [SerializeField] private float maxLookAngle = 80f;

    private void Start()
    {
        if(animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if(_allowMove)
        {
            //Vector2 dir = joystick.Direction;
            //bool isIdle = dir.x == 0 && dir.y == 0;

            HandleMove();
            HandleLook();

            //if (isIdle)
            //{
            //    SetAnimation("idle");
            //} else
            //{
            //    SetAnimation("move");
            //}
        }
    }

    void HandleMove()
    {
        Vector2 input = joystick.Direction;

        Vector3 move = transform.right * input.x + transform.forward * input.y;

        if (move.magnitude > 1f)
            move.Normalize();

        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private float xRotation = 0f;
    void HandleLook()
    {
        Vector2 lookInput = joystickRotation.Direction;

        float mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * lookSensitivity * Time.deltaTime;

        // X rotation (camera up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        //cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Y rotation (player left/right)
        model.Rotate(Vector3.up * mouseX);
    }

    private void SetAnimation(string name)
    {
        if (name == _currentAnim) return;
        animator.ResetTrigger(_currentAnim);
        _currentAnim = name;
        animator.SetTrigger(_currentAnim);

    }
}
