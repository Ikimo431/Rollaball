using System;
using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
   private  Rigidbody rb;
   private PlayerController pc;
   public float jumpForce;
   public float dashForce;
   public float dashCooldownSeconds;
   private float dashCooldown = 0;

   void Start()
   {
      rb=GetComponent<Rigidbody>();
      pc = GetComponent<PlayerController>();
   }

   void Update()
   {
      if (dashCooldown - Time.deltaTime > 0)
      {
         dashCooldown -= Time.deltaTime;
      }
      else {dashCooldown = 0;}
   }

   public void Jump()
   {
      rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
      pc.setGrounded(false);
   }
   
   public void Dash(Vector3 dir)
   {
      if (dashCooldown == 0)
      {
         rb.AddForce(dir*dashForce, ForceMode.Impulse);
         dashCooldown = dashCooldownSeconds;
      }
      
      //StartCoroutine(dashDelay(dir));
   }
   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         pc.setGrounded(true);
      }
   }

   //----NOTE scraping freze, but leaving stuff here as example
   /*
   IEnumerator dashDelay(Vector3 direction)
   {
      if (!dashing)
      {
         dashing = true;
         
         rb.constraints = RigidbodyConstraints.FreezePosition;
         transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
         yield return new WaitForSeconds(0.25f);
         rb.constraints = RigidbodyConstraints.None;
         rb.AddForce(direction * dashForce, ForceMode.Impulse);
         dashing = false;
      }
      
   } */

  
}
