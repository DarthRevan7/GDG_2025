using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;
    private Rigidbody rb;
    private Animator animator;

    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (cameraTransform == null)
        {
            Camera cam = Camera.main;
            if (cam != null)
                cameraTransform = cam.transform;
            else
                Debug.LogWarning("CameraTransform non assegnato!");
        }
    }

    void Update()
    {
        // Input da tastiera
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = cameraTransform.right;

        moveDirection = (camForward * v + camRight * h).normalized;

        // Imposta il parametro dell'animator
        if (animator != null)
        {
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

        // Ruota il personaggio se c'è movimento
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void FixedUpdate()
    {
        // Applica movimento fisico
        Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }
}
