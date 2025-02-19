﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float rotationSpeed = 8f;
    [SerializeField] float gravity = 20f;

    // State
    Vector3 movementVector = Vector3.zero;
    bool isRunning = false;

    // Cached Components
    CharacterController controller = null;
    Animator animator = null;
    Camera mainCamera = null;
    AudioManager audioManager = null;
    AudioSource walkingAudio = null;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        audioManager = AudioManager.AudioManagerInstance;

        walkingAudio = audioManager.PlaySound(AudioManager.SoundKey.PlayerWalk, transform.position);
        walkingAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded) 
        {
            HandleMovementInput();
            HandleMovementDirection();
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
            if (!isRunning)
            {
                if(walkingAudio && !walkingAudio.isPlaying) walkingAudio.Play();

                Debug.Log("Playing sound");
                isRunning = true;
            }
           
        }
        else if (vertical < 0f)
        {
            movementVector = -transform.forward * movementSpeed;
            animator.SetBool("isRunning", false);
            
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningBack", true);
            if (!isRunning)
            {
                if (walkingAudio && !walkingAudio.isPlaying) walkingAudio.Play();

                Debug.Log("Playing sound");
                isRunning = true;
            }
        }
        else if (horizontal < 0f)
        {
            movementVector = -transform.right * movementSpeed;
            animator.SetBool("isRunning", false);
            
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningLeft", true);
            animator.SetBool("isRunningBack", false);
            if (!isRunning)
            {
                if (walkingAudio && !walkingAudio.isPlaying) walkingAudio.Play();

                Debug.Log("Playing sound");
                isRunning = true;
            }
        }
        else if (horizontal > 0f)
        {
            movementVector = transform.right * movementSpeed;
            animator.SetBool("isRunning", false);
            
            animator.SetBool("isRunningRight", true);
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningBack", false);
            if (!isRunning)
            {
                if (walkingAudio && !walkingAudio.isPlaying) walkingAudio.Play();
                Debug.Log("Playing sound");
                isRunning = true;
            }
        }
        else
        {
            movementVector = Vector3.zero;
            animator.SetBool("isRunning", false);
            
            if (isRunning)
            {
                if (walkingAudio && walkingAudio.isPlaying) walkingAudio.Stop();
                Debug.Log("Stopping sound");
                isRunning = false;
            }
            
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

    private void OnDisable()
    {
        if (walkingAudio && walkingAudio.isPlaying) walkingAudio.Stop();
    }
}
