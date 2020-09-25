using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] float firePower = 50f;

    // State
    [SerializeField] TargetCode projectileToShootCode = TargetCode.Blue;
    [SerializeField] Material projectileMaterial = null;

    // Cached Components
    TowerController towerController;
    ProjectilePool projectilePool;

    // Start is called before the first frame update
    void Start()
    {
        towerController = GetComponentInParent<TowerController>();
        projectilePool = FindObjectOfType<ProjectilePool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!towerController.IsActivated) return;
        if (Input.GetMouseButtonDown(0))
        {
            Projectile projectileToShoot = projectilePool.GetProjectile(); 
            if(projectileToShoot)
            {
                projectileToShoot.transform.position = transform.position;
                projectileToShoot.target = projectileToShootCode;
                if(projectileMaterial) projectileToShoot.meshRenderer.material = projectileMaterial;
                projectileToShoot.rigidBody.AddForce(transform.forward * firePower, ForceMode.Impulse);
            }
        }
    }
}
