using System;
using UnityEngine;

public class DashTipScript : MonoBehaviour
{
    private GameObject self; 
    private void Start()
    {
        self = this.gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Destroy(self);
        }
        
    }
}
