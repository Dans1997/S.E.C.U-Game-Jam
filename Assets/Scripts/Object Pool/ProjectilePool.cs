using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] int objectNumber = 50;
    [SerializeField] Projectile projectilePrefab = null;

    // State
    Queue<Projectile> projectilePool = new Queue<Projectile>();

    // Start is called before the first frame update
    void Start()
    {
        for (var i=0; i < objectNumber; i++)
        {
            Projectile newProjectile = Instantiate(projectilePrefab, transform);
            Rigidbody newRigidbody = newProjectile.gameObject.AddComponent<Rigidbody>();
            MeshRenderer newMeshRenderer = newProjectile.gameObject.AddComponent<MeshRenderer>();

            newProjectile.rigidBody = newRigidbody;
            newProjectile.rigidBody.constraints = RigidbodyConstraints.FreezeRotationY;
            newProjectile.meshRenderer = newMeshRenderer;
            newProjectile.gameObject.SetActive(false);
            projectilePool.Enqueue(newProjectile);
        }

        // Event Handler
        Projectile.OnProjectileCollision += ProjectileCollisionHandler;
    }

    private void ProjectileCollisionHandler(Projectile projectileToPool)
    {
        projectileToPool.rigidBody.velocity = Vector3.zero;
        projectileToPool.rigidBody.rotation = Quaternion.identity;
        projectileToPool.gameObject.SetActive(false);
        projectilePool.Enqueue(projectileToPool);
    }

    public Projectile GetProjectile()
    {
        if (projectilePool.Count <= 0) return null;
        Projectile projectileToReturn = projectilePool.Dequeue();
        projectileToReturn.rigidBody.velocity = Vector3.zero;
        projectileToReturn.gameObject.SetActive(true);
        return projectileToReturn;
    }

    void OnDestroy()
    {
        Projectile.OnProjectileCollision -= ProjectileCollisionHandler;
    }
}