using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float firePower = 50f;

    // Cached Components
    TowerController towerController;

    // Start is called before the first frame update
    void Start()
    {
        towerController = GetComponentInParent<TowerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!towerController.IsActivated) return;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody projectileBody = projectile.GetComponent<Rigidbody>();
            projectileBody.AddForce(transform.forward * firePower, ForceMode.Impulse);
        }
    }
}
