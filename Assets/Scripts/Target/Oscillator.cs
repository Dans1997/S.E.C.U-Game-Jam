using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] Vector3 movementVector = Vector3.zero;
    [SerializeField] float period = 2f;

    // State
    Vector3 startingPos = Vector3.zero;
    float movementFactor = 0f;
    float sign = 1f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        sign = Mathf.Sign(Random.Range(-1f, 1f));
        movementVector *= sign;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;

        const float TAU = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * TAU);

        movementFactor = rawSinWave / 2f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
