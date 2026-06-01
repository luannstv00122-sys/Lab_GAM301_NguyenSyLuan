using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMove : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private PlayerInput playerInput;
    private InputAction lookAction;

    float xRotation = 0f;
    float yRotation = 0f;

    void Awake()
    {
        playerInput = playerBody.GetComponent<PlayerInput>();
        lookAction = playerInput.actions["Look"];
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        yRotation += mouseX;

        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}