using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Object = UnityEngine.Object;

public class PlayerController : MonoBehaviour
{
    private PlayerAbilities abilities;
    private Rigidbody rb;
    private int count;

    private float movementX;
    private float movementY;
    public float speed;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int reaminingPickUps;

    private bool grounded;

    public void setGrounded(bool grounded)
    {
        this.grounded = grounded;
    }
 
    public event Action OnPlayerDeath;
    void Start()
    {
        abilities = GetComponent<PlayerAbilities>();
       rb = GetComponent<Rigidbody>();
       count = 0;
       SetCountText();
       winTextObject.SetActive(false);
       Type pickUpType = Type.GetType("Rotator");
       reaminingPickUps = UnityEngine.Object.FindObjectsByType(pickUpType, FindObjectsSortMode.None).Length;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            abilities.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //TODO none of the getkeydown are firing, so not dashing in accordance with which directional key is 
            //pressed with the dash key
            if (Input.GetKeyDown(KeyCode.W)){abilities.Dash(Vector3.forward);}
            else if (Input.GetKeyDown(KeyCode.D)){abilities.Dash(Vector3.right);}
            else if (Input.GetKeyDown(KeyCode.A)){abilities.Dash(Vector3.left);}
            else if (Input.GetKeyDown(KeyCode.S)){abilities.Dash(Vector3.back);}
            else {abilities.Dash(Vector3.forward);}
        }
        
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
        if (rb !=null)
        {
            rb.AddForce(movement*speed);
        }
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
            GameLoss();
        }
    }

    void GameLoss()
    {
        Destroy(gameObject);
        OnPlayerDeath?.Invoke();
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose...";
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    
}
