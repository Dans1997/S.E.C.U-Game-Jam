using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTargetSpawner : MonoBehaviour
{
    // State 
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;

    [Header("Target Specs")]
    [SerializeField] int targetToSpawnScoreValue = 10;

    [Header("Material Array (Red, Green, Blue, Black)")]
    [SerializeField] Material[] materialArray = null; // In order of index

    // Cached Components
    Target currentTarget = null;
    TargetPool targetPool = null;
    Coroutine spawnTargetCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        targetPool = FindObjectOfType<TargetPool>();

        spawnTargetCoroutine = StartCoroutine(SpawnTarget());

        // Event Handler
        Target.OnTargetDestroy += TargetHitHandler;
    }

    private void TargetHitHandler(Target targetToPool)
    {
        if (spawnTargetCoroutine != null) StopCoroutine(spawnTargetCoroutine);
        spawnTargetCoroutine = StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        int targetCode = Random.Range((int)TargetCode.Red, (int)TargetCode.Black + 1);
        TargetCode targetToSpawnCode = (TargetCode) targetCode;
        yield return new WaitForSeconds(spawnDelay);
        if (currentTarget == null)
        {
            currentTarget = targetPool.GetTarget();
            if (currentTarget)
            {
                currentTarget.transform.position = transform.position;
                currentTarget.transform.localScale = new Vector3(10, 10, 10); // Temporary
                currentTarget.code = targetToSpawnCode;
                if (materialArray[targetCode]) currentTarget.meshRenderer.material = materialArray[targetCode];
                currentTarget.scoreValue = targetToSpawnScoreValue;
            }
        }
    }
}
