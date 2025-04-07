using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private int defaultColliders = 1, actualColliders = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
