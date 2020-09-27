using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    [SerializeField] Target targetPrefab;

    // State
    int objectNumber = 50;
    Queue<Target> targetPool = new Queue<Target>();

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

        // Event Handler
        Target.OnTargetDestroy += TargetHitHandler;
    }

    private void TargetHitHandler(Target targetToPool)
    {
        targetToPool.gameObject.SetActive(false);
        targetPool.Enqueue(targetToPool);
    }

    public Target GetTarget()
    {
        if (targetPool.Count <= 0) return null;
        Target targetToReturn = targetPool.Dequeue();
        targetToReturn.gameObject.SetActive(true);
        return targetToReturn;
    }
}
