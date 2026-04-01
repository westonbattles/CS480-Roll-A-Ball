using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // distance between camera and player
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after all other updates
    void LateUpdate()
    {
        // set position to the players position + the initial offset
        transform.position = player.transform.position + offset;
    }
}
