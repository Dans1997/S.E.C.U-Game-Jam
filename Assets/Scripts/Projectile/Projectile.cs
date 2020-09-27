using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    // State
    public TargetCode target;

    // Cached Components
    public Rigidbody rigidBody = null;
    public MeshRenderer meshRenderer = null;

    // Collision Event Notifier
    public static event Action<Projectile> OnProjectileCollision;

    private void OnCollisionEnter(Collision collision)
    {
        Target targetHit = collision.gameObject.GetComponentInParent<Target>();
        if (targetHit && targetHit.code == target)
        {
            targetHit.OnTargetHit();
        }

        // If the projectile collides with anything
        OnProjectileCollision?.Invoke(this);
    }

    // If the projectile leaves the map, it will hit a collider with the isTrigger activated
    private void OnTriggerEnter(Collider collider)
    {
        OnProjectileCollision?.Invoke(this);
    }
}