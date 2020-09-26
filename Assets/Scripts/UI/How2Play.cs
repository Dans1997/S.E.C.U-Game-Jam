using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class How2Play : MonoBehaviour
{

    public GameObject menuObject;
    public GameObject how2PlayObject;

    public void showHow2Play()
    {
        menuObject.SetActive(false);
        how2PlayObject.SetActive(true);
    }

    public void exitHot2Play()
    {
        how2PlayObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
