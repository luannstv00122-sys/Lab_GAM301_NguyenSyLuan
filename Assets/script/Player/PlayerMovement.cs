using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    private CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;

    private float velocityY;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 move = transform.right * input.x + transform.forward * input.y;

        controller.Move(move * speed * Time.deltaTime);

        // ===== GRAVITY =====
        if (controller.isGrounded && velocityY < 0)
        {
            velocityY = -2f; // giữ chặt xuống đất
        }

        velocityY += gravity * Time.deltaTime;

        controller.Move(Vector3.up * velocityY * Time.deltaTime);
    }
}