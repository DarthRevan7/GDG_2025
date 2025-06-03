using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonRigidbodyController : MonoBehaviour
{
    public float velocity = 5f;
    public float sprintAdittion = 3.5f;
    public float jumpForce = 7f; // Usiamo forza realistica (modificato)
    public float gravityMultiplier = 2f;

    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    Animator animator;
    Rigidbody rb;
    PlatformFollower platformFollower;

    bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        platformFollower = GetComponent<PlatformFollower>();

        rb.freezeRotation = true;

        if (animator == null)
            Debug.LogWarning("Animator non trovato nel player.");
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetButtonDown("Jump");
        inputSprint = Input.GetButton("Fire3");
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);

        if (inputCrouch)
            isCrouching = !isCrouching;

        // Controllo su piattaforme mobili
        isGrounded = CheckIfGrounded();

        if (animator != null)
        {
            animator.SetBool("crouch", isCrouching);

            Vector3 horizontalVelocity = rb.linearVelocity;
            horizontalVelocity.y = 0;
            bool isActuallyMoving = horizontalVelocity.magnitude > 0.1f;
            animator.SetBool("run", isActuallyMoving && inputVertical != 0f);

            isSprinting = isActuallyMoving && inputSprint;
            animator.SetBool("sprint", isSprinting);

            animator.SetBool("air", !isGrounded);
        }

        if (inputJump && isGrounded)
        {
            isJumping = true;
        }

        HeadHittingDetect();
    }

    void FixedUpdate()
    {
        float currentSpeed = velocity;
        if (isSprinting) currentSpeed += sprintAdittion;
        if (isCrouching) currentSpeed *= 0.5f;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * inputHorizontal + forward * inputVertical).normalized;

        Vector3 targetVelocity = moveDirection * currentSpeed;
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 velocityChange = targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
        }

        if (isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset Y per salti precisi
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        // Extra gravity per caduta più realistica
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    bool CheckIfGrounded()
    {
        float rayDistance = 0.2f;
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, rayDistance) ||
               (platformFollower != null && platformFollower.IsOnPlatform);
    }

    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 origin = transform.position + Vector3.up * 1f;
        if (Physics.Raycast(origin, Vector3.up, headHitDistance))
        {
            // Reset jump o forza verso il basso
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Min(rb.linearVelocity.y, 0), rb.linearVelocity.z);
        }
    }
}
