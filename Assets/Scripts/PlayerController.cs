using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // implicit self get component
        count = 0;
        winTextObject.SetActive(false);

        SetCountText();
    }

    // Physics processees
    void FixedUpdate() 
    {   
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }

        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
   }
}
