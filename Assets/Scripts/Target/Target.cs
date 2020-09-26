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

    public static event Action<Target> OnTargetHitEvent;

    public void OnTargetHit()
    {
        OnTargetHitEvent?.Invoke(this);
    }
}
