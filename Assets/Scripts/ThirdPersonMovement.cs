using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] Transform thirdPersonCamera;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float rotationSpeed = 8f;
    [SerializeField] float gravity = 20f;

    [SerializeField] float turnSmoothTime = 0.1f;

    // State
    Vector3 movementVector = Vector3.zero;
    float turnSmoothVelocity;

    // Cached Components
    CharacterController controller = null;
    Camera mainCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleMovementDirection();

        // Move Player
        movementVector.y -= gravity;
        controller.Move(movementVector * Time.deltaTime);
    }

    private void HandleMovementInput()
    {
        if (!controller.isGrounded) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // Only move forward
        if (vertical >= Mathf.Epsilon)
        {
            movementVector = transform.forward * movementSpeed;
        }
        else
        {
            movementVector = Vector3.zero;
        }
    }

    private void HandleMovementDirection()
    {
        Vector3 cameraForwardDirection = mainCamera.transform.forward;
        Vector3 directionToMoveIn = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));

        Debug.DrawRay(Camera.main.transform.position, cameraForwardDirection * 10, Color.red);
        Debug.DrawRay(Camera.main.transform.position, directionToMoveIn * 10, Color.blue);

        float desiredRotationAngle = Vector3.Angle(transform.forward, directionToMoveIn);
        float crossProduct = Vector3.Cross(transform.forward, directionToMoveIn).y;

        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }

        if (Mathf.Abs(desiredRotationAngle) > 10)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * rotationSpeed * Time.deltaTime);
        }
    }
}
