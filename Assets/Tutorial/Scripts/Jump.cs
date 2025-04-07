using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private string tagSafeZone = "SafeZone", tagDangerZone = "Lava";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Is called when the object has a collision with another one
    void OnCollisionEnter(Collision collision)
    {
        // If the other object is a safe zone, just set isgrounded to true
        if(collision.gameObject.tag.Equals(tagSafeZone))
        {
            isGrounded = true;
        }
        // If the other object is not a safe zone then the player must DIE.
        else if(collision.gameObject.tag.Equals(tagDangerZone))
        {
            Debug.Log("DEAD!");
            // Delete next instruction later
            isGrounded = true;
        }
    }

    private void JumpHandling()
    {
        // If the player wants to jump and the character is grounded
        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            // Add a force to the rigidbody component. The force type is impulse
            // Then set isgrounded as false.
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        JumpHandling();
    }
}
