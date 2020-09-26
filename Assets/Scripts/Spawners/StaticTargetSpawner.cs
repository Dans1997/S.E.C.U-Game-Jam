using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTargetSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;

    [Header("Target Specs")]
    [SerializeField] int targetToSpawnScoreValue = 10;

    [Header("Material Array (Red, Green, Blue, Black)")]
    [SerializeField] Material[] materialArray = null; // In order of index

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
        if (transform.childCount != 1)
        {
            StartCoroutine(SpawnTarget());
        }
    }

    IEnumerator SpawnTarget()
    {
        currentTarget = targetPool.GetTarget();
        if (!currentTarget) yield break;
        currentTarget.transform.parent = transform;

        float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

        int targetCode = Random.Range((int)TargetCode.Red, (int)TargetCode.Black + 1);
        TargetCode targetToSpawnCode = (TargetCode) targetCode;
        yield return new WaitForSeconds(spawnDelay);

        currentTarget.transform.localPosition = Vector3.zero;
        currentTarget.transform.localScale = new Vector3(1, 1, 1);
        currentTarget.code = targetToSpawnCode;
        if (materialArray[targetCode]) currentTarget.meshRenderer.material = materialArray[targetCode];
        currentTarget.scoreValue = targetToSpawnScoreValue;
    }
}
