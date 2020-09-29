using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [Header("Projectile Variables")]
    [SerializeField] float firePower = 200f;
    [SerializeField] TargetCode projectileToShootCode = TargetCode.Blue;
    [SerializeField] Material projectileMaterial = null;

    // Cached Components
    TowerController towerController;
    ProjectilePool projectilePool;
    CameraShake3D cameraShake3D;

    // State
    bool hasShot = false;
    float cooldownInSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        towerController = GetComponentInParent<TowerController>();
        projectilePool = FindObjectOfType<ProjectilePool>();
        cameraShake3D = GetComponent<CameraShake3D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!towerController.IsActivated) return;
        if (Input.GetMouseButtonDown(0) && !hasShot)
        {
            hasShot = true;
            Projectile projectileToShoot = projectilePool.GetProjectile(); 
            if(projectileToShoot)
            {
                AudioManager.AudioManagerInstance.PlaySound(AudioManager.SoundKey.ProjectileOne);
                projectileToShoot.transform.position = transform.position;
                projectileToShoot.rigidBody.angularVelocity = Vector3.zero;
                projectileToShoot.rigidBody.rotation = Quaternion.LookRotation(transform.forward, transform.up);
                projectileToShoot.target = projectileToShootCode;
                if(projectileMaterial) projectileToShoot.meshRenderer.material = projectileMaterial;
                projectileToShoot.rigidBody.AddForce(transform.forward * firePower, ForceMode.Impulse);
            }

            // Shake Camera
            cameraShake3D.ShakeOnce(0.3f, 0.1f);

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownInSeconds);
        hasShot = false;
    }
}
