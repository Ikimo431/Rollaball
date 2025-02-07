using System;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour
{
    public event Action WeakSpotHit;
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            WeakSpotHit?.Invoke();
            Disable();
            c.rigidbody.AddForce(transform.forward*8f, ForceMode.Impulse);
        }
    }

    public void Enable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }
}
