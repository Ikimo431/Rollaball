using System;
using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public BossWeakPoint[] BossWeakPoints;
    public int health;
    public int knockbackForce;
    private int currWeakPtIndex;
    private Rigidbody rb;
    public GameObject bossDrop;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < BossWeakPoints.Length; i++)
        {
            BossWeakPoints[i].WeakSpotHit += TakeDamage;
            BossWeakPoints[i].Disable();
        }

        BossWeakPoints[0].Enable();
        
        //rb = GetComponent<Rigidbody>();

    }
    
    void ActivateRandomWeakPoint(int prevActiveWeakPoint)
    {
        while (currWeakPtIndex == prevActiveWeakPoint)
        { 
            currWeakPtIndex = UnityEngine.Random.Range(0, BossWeakPoints.Length);
            Debug.Log(currWeakPtIndex);
        }
        
        BossWeakPoints[currWeakPtIndex].Enable();
    }
    private void TakeDamage()
    {
        health--;
        Debug.Log(health);
        if (health <= 0)
        {
            Instantiate((bossDrop), transform.position, transform.rotation);
            Destroy(gameObject);
        }
        ActivateRandomWeakPoint(currWeakPtIndex);
        //StartCoroutine(knockBackStun());
    }
    
    IEnumerator knockBackStun()
    {
        rb.AddForce(
            -knockbackForce * (BossWeakPoints[currWeakPtIndex].transform.forward)
        );
        yield return new WaitForSeconds(1f); // time until freeze
    }
}
