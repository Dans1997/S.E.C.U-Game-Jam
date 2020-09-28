using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float rotationSpeed = 8f;
    [SerializeField] float gravity = 20f;
    [SerializeField] float jumpForce = 600f;

    // State
    Vector3 movementVector = Vector3.zero;

    // Cached Components
    CharacterController controller = null;
    Camera mainCamera = null;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded) 
        {
            HandleMovementInput();
            HandleMovementDirection();
            HandleMovementJump();
        }
        
        movementVector.y -= gravity * Time.deltaTime;
        controller.Move(movementVector * Time.deltaTime);
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (vertical > 0f)
        {
            movementVector = transform.forward * movementSpeed;
            animator.SetBool("isRunning", true);
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningBack", false);
        }
        else if (vertical < 0f)
        {
            movementVector = -transform.forward * movementSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningBack", true);
        }
        else if (horizontal < 0f)
        {
            movementVector = -transform.right * movementSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningLeft", true);
            animator.SetBool("isRunningBack", false);
        }
        else if (horizontal > 0f)
        {
            movementVector = transform.right * movementSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isRunningRight", true);
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningBack", false);
        }
        else
        {
            movementVector = Vector3.zero;
            movementVector = transform.right * movementSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningBack", false);
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

    private void HandleMovementJump()
    {
        if (Input.GetButton("Jump"))
        {
            movementVector.y = jumpForce;
        }
    }
}
