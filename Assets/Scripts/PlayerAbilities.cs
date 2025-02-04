using System;
using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
   private  Rigidbody rb;
   private PlayerController pc;
   public float jumpForce;
   public float dashForce;
   private bool dashing;
   void Start()
   {
      rb=GetComponent<Rigidbody>();
      pc = GetComponent<PlayerController>();
   }

   void Update()
   {
      Debug.DrawRay(transform.position, new Vector3(transform.forward.x, 0, transform.forward.z) * 10f, Color.red); 
   }

   public void Jump()
   {
      rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
      pc.setGrounded(false);
   }
   

   public void Dash(Vector3 dir)
   {
      StartCoroutine(dashDelay(dir));
   }

   IEnumerator dashDelay(Vector3 direction)
   {
      rb.constraints = RigidbodyConstraints.FreezePosition;
      transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
      yield return new WaitForSeconds(1);
      
      rb.constraints = RigidbodyConstraints.None;
      rb.AddForce(direction * dashForce, ForceMode.Impulse);
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         pc.setGrounded(true);
      }
   }
}
