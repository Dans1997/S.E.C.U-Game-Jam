﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    [SerializeField] Target targetPrefab = null;

    // State
    [SerializeField]int objectNumber = 120;
    Queue<Target> targetPool = new Queue<Target>();

    // Cached Components
    AudioManager audioManager = null;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < objectNumber; i++)
        {
            Target newTarget = Instantiate(targetPrefab, transform);
            MeshRenderer newMeshRenderer = newTarget.gameObject.AddComponent<MeshRenderer>();

            newTarget.meshRenderer = newMeshRenderer;
            newTarget.gameObject.SetActive(false);
            targetPool.Enqueue(newTarget);
        }

        audioManager = AudioManager.AudioManagerInstance;

        // Event Handler
        Target.OnTargetHitEvent += PoolTarget;
    }

    private void PoolTarget(Target targetToPool)
    {
        if (!targetToPool)
        {
            Debug.LogWarning("(TargetPool) Can't pool a null object.");
            return;
        }
        targetToPool.transform.parent = transform;
        targetToPool.gameObject.SetActive(false);
        targetPool.Enqueue(targetToPool);
    }

    public Target GetTarget()
    {
        if (targetPool.Count <= 0) return null;
        Target targetToReturn = targetPool.Dequeue();
        return targetToReturn;
    }

    void OnDestroy()
    {
        Target.OnTargetHitEvent -= PoolTarget;
    }
}