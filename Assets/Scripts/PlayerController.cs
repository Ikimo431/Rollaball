using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Object = UnityEngine.Object;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;

    private float movementX;
    private float movementY;
    public float speed;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int reaminingPickUps;
 
    
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       count = 0;
       SetCountText();
       winTextObject.SetActive(false);
       Type pickUpType = Type.GetType("Rotator");
       reaminingPickUps = UnityEngine.Object.FindObjectsByType(pickUpType, FindObjectsSortMode.None).Length;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,0.0f, movementY);
        rb.AddForce(movement*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            Destroy(other.gameObject);
            count++;
            SetCountText();
            
            Type pickUpType = Type.GetType("Rotator"); // pickup has rotator class from Rotator script 
            reaminingPickUps = UnityEngine.Object.FindObjectsByType(pickUpType, FindObjectsSortMode.None).Length-1;
            //-1 because occurs synchronously, before pickup is destroyed
            Debug.Log(reaminingPickUps);
            if (reaminingPickUps == 0) //WIN CONDITION
            {
                winTextObject.SetActive(true);
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            }
        }
        
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(("Enemy"))) //LOSE CONDITION
        {

            Destroy(gameObject);
            
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose...";
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    
}
