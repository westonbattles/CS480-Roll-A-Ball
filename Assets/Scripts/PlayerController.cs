using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using NUnit.Framework.Constraints;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public int extraJumps = 1;
    public float jumpForce;
    public Vector3 feetPosition = new Vector3(0f, -0.48f, 0f);
    public LayerMask groundLayer;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count = 0;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float jumpsRemaining;
    private bool shouldJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // implicit self get component
        winTextObject.SetActive(false);

        SetCountText();

        jumpsRemaining = extraJumps;
    }

    // Physics processees
    void FixedUpdate() 
    {   
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        bool grounded = isGrounded();

        rb.AddForce(movement * speed);        

        // reset jumps when we are grounded
        if (grounded) { jumpsRemaining = extraJumps; }

        if (shouldJump)
        {
            if (grounded || jumpsRemaining > 0)
            {
                rb.AddForce(Vector3.up * jumpForce);
                if (!grounded) jumpsRemaining -= 1; // if we performed extra jump
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
