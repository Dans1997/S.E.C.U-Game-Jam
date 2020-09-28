using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall4 : MonoBehaviour
{

    [SerializeField] float timeToRaise;
    [SerializeField] float timeToDown;

    private float yInc = 1f;

    void Start()
    {
        StartCoroutine(RaiseWall());
    }

    IEnumerator RaiseWall()
    {
        // Wait Xs before raise the wall
        yield return new WaitForSeconds(timeToRaise);

        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            transform.position += new Vector3 (0f, yInc, 0f);

            if (transform.position.y >= 11)
            {
                StartCoroutine(DownWall());
                yield break;
            }
        }
    }

    IEnumerator DownWall()
    {
        // Wait Xs before go down the wall
        yield return new WaitForSeconds(timeToDown);

        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            transform.position += new Vector3 (0f, -yInc, 0f);

            if (transform.position.y <= -17f)
            {
                StartCoroutine(RaiseWall());
                yield break;
            }
        }
    }

}
