using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // State
    public TargetCode code;
    public int scoreValue = 10;

    // Cached Components
    public MeshRenderer meshRenderer = null;

    public static event Action<Target> OnTargetDestroy;

    private void OnDestroy() => OnTargetDestroy?.Invoke(this);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
