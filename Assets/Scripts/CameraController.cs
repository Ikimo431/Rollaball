using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerController player;
    private Vector3 offset;
    
    void Start()
    {
        offset=transform.position-player.transform.position;
        player.OnPlayerDeath += playerDeathEvent;
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
