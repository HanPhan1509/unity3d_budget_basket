using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform model;

    [Header("Movement")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look")]
    [SerializeField] private Joystick joystickRotation;
    [SerializeField] private float lookSensitivity = 120f;
    [SerializeField] private float maxLookAngle = 80f;

    private float xRotation = 0f;
    private bool _allowMove = true;
    private string _currentAnim = "idle";
    private Vector3 velocity;

    private void Start()
    {
        this.m_Camera.SetActive(true);
        if (animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if(_allowMove)
        {
            HandleMove();
            HandleLook();

            //if (isIdle)
            //{
            //    SetAnimation("idle");
            //} else
            //{
            //    SetAnimation("move");
            //}
        } else
        {
            controller.Move(Vector3.zero);
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

    public void StopMove()
    {
        this.m_Camera.SetActive(false);
        this._allowMove = false;
    }
}