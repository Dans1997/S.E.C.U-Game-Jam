using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetCode code;
    [SerializeField] private int scoreValue;

    public static event Action<int> OnTargetDestroy;

    private void OnDestroy() => OnTargetDestroy?.Invoke(this.scoreValue);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
