using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


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
    
    public event Action OnPlayerDeath;// listened by CameraController
    
    //-----BUILT IN FUNCTIONS-----------
    void Start()
    {
        abilities = GetComponent<PlayerAbilities>();
       rb = GetComponent<Rigidbody>();
       count = 0;
       winTextObject.SetActive(false);
       SetCountText();
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
            
            float totVelocityMagnitude = Math.Abs(rb.linearVelocity.x) + Math.Abs(rb.linearVelocity.z);
            float xPercent = rb.linearVelocity.x / totVelocityMagnitude;
            float zPercent = rb.linearVelocity.z / totVelocityMagnitude;
            Vector3 dir = new Vector3(xPercent, 0, zPercent);
            abilities.Dash(dir);
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
                StartCoroutine(toMenuTimer());
            }
        }
    }

    IEnumerator toMenuTimer()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy")) //LOSE CONDITION
        {
            GameLoss();
        }
    }

    void GameLoss()
    {
        Destroy(gameObject);
        OnPlayerDeath?.Invoke(); 
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose...\n\n Press Esc To Return To Menu";
    }
    
    //----GETTERS/SETTERS---------------
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    public void setGrounded(bool grounded)
    {
        this.grounded = grounded;
    }
    
}
