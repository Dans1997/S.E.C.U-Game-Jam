using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] TargetCode target;

    private void OnCollisionEnter(Collision collision)
    {
        Target hitObj = collision.gameObject.GetComponentInParent<Target>();
        if (hitObj && hitObj.code == target)
        {
            Destroy(hitObj.gameObject);
            Debug.Log("Hit: " + collision.transform.name);
        }
    }
}
