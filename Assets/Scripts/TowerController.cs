using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Tower Camera Look Controls
    float mouseSensitivity = 100f;
    float xRotation = 0f;

    // Activation Controls
    float towerRange = 10f;

    // Cached Components
    ThirdPersonMovement player = null;
    Camera towerCamera = null;
    CameraSelector cameraSelector;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonMovement>();
        towerCamera = GetComponentInChildren<Camera>();
        cameraSelector = CameraSelector.CameraSelectorInstance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleActivation();
        if (!towerCamera.enabled) return;
            
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        towerCamera.transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.y, transform.localRotation.z);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleActivation()
    {
        bool pressedSpaceThisFrame = Input.GetKeyDown(KeyCode.Space);

        if (towerCamera.enabled && pressedSpaceThisFrame)
        {
            cameraSelector.ReturnToThirdView();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer <= towerRange && pressedSpaceThisFrame)
        {
            cameraSelector.SwitchToCamera(towerCamera);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
