using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTargetSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;
    [SerializeField] bool canRespawnTargets = false;

    [Header("Target Specs")]
    [SerializeField] int targetToSpawnScoreValue = 10;

    [Header("Material Array (Red, Green, Blue, Black)")]
    [SerializeField] Material[] materialArray = null; // In order of index

    // State
    bool hasSpawnedOnce = false;

    // Cached Components
    TargetPool targetPool = null;
    Target currentTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        targetPool = FindObjectOfType<TargetPool>();
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1 && canRespawnTargets)
        {
            //Debug.Log("Respawning");
            StartCoroutine(SpawnTarget());
        }
        else if (transform.childCount == 1 && !hasSpawnedOnce && !canRespawnTargets)
        {
            hasSpawnedOnce = true;
        }
        else if (transform.childCount > 1)
        {
            Debug.LogWarning("Static spawner should not have more than one child target.");
        }
    }

    IEnumerator SpawnTarget()
    {
        currentTarget = targetPool.GetTarget();
        if (!currentTarget) yield break;
        currentTarget.transform.parent = transform;
        currentTarget.transform.localPosition = Vector3.zero;

        if (!hasSpawnedOnce)
        {
            SetTarget();
            hasSpawnedOnce = true;
            yield break;
        }

        if (canRespawnTargets)
        {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
        }

        SetTarget();
    }

    private void SetTarget()
    {
        int targetCode = Random.Range((int)TargetCode.Red, (int)TargetCode.Black + 1);
        TargetCode targetToSpawnCode = (TargetCode)targetCode;

        currentTarget.gameObject.SetActive(true);
        currentTarget.transform.localScale = new Vector3(1, 1, 1);
        currentTarget.code = targetToSpawnCode;
        if (materialArray[targetCode]) currentTarget.meshRenderer.material = materialArray[targetCode];
        currentTarget.scoreValue = targetToSpawnScoreValue;
    }
}