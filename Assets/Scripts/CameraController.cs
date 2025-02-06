using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    public PlayerController player;
    private Vector3 offset;
    
    void Start()
    {
        offset=transform.position-player.transform.position;
        player.OnPlayerDeath += playerDeathEvent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); //in camera because player destroyed on death
        }
    }
   
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
        
            
    }
    public void playerDeathEvent()
    {
        player = null;
    }
}
