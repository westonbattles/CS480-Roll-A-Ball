using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Vector3 feetPosition = new Vector3(0f, -0.48f, 0f);
    public LayerMask groundLayer;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int extraJumps = 0;
    private bool shouldJump = false;

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

        if (shouldJump)
        {
            // catch frame where the game thinks we are grounded but we've already added jump force
            if (isGrounded() )
            {
                rb.AddForce(Vector3.up * jumpForce);
                Debug.Log("JUMPED!");
            }

            shouldJump = false;
        }
        
        
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

    void OnJump(InputValue movementValue)
    {
        shouldJump = true;
    }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
   }

   bool isGrounded()
    {
        return Physics.Raycast(transform.position + feetPosition, Vector3.down, 0.1f, groundLayer);
    }
}
