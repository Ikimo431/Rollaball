using System;
using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Transform spinThing;
    public int rotateSpeed;
    private bool spinning = false;

    private void Start()
    {
        StartCoroutine(spinWithDelay());
    }

    void Update()
    {
        if (spinning)
        {
            spinThing.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
        }
    }

    IEnumerator spinWithDelay()
    {
        spinning = true;
        yield return new WaitForSeconds(3f); //the length of time to spin for 
        spinning = false;
        yield return new WaitForSeconds(1); //the time between spins
        StartCoroutine(spinWithDelay());
    }
}
