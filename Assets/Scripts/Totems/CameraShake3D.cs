using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake3D : MonoBehaviour
{
    // State
    Vector3 startingPos = Vector3.zero;
    float rotationMultiplier = 5f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;
    }

    public void ShakeOnce(float duration, float strength)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCamera(duration, strength));
    }

    public IEnumerator ShakeCamera(float duration, float strength)
    {
        float endTime = Time.time + duration;
        float fadeTime = strength / duration;
        float power = strength;
        float rotationPower = strength * rotationMultiplier;

        while (Time.time < endTime)
        {
            transform.localPosition = startingPos + Random.insideUnitSphere * power;

            duration -= Time.deltaTime;

            power = Mathf.MoveTowards(power, 0f, fadeTime * Time.deltaTime);
            //rotationPower = Mathf.MoveTowards(rotationPower, 0f, fadeTime * rotationMultiplier * Time.deltaTime);
            yield return null; // once every frame 
        }

        transform.localPosition = startingPos;
    }
}
