using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float velocity = 5f;
    public float sprintAdittion = 3.5f;
    public float jumpForce = 18f;
    public float jumpTime = 0.85f;
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    Animator animator;
    CharacterController cc;
    PlatformFollower platformFollower;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        platformFollower = GetComponent<PlatformFollower>();

        if (animator == null)
            Debug.LogWarning("Animator non trovato nel player.");
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Jump") == 1f;
        inputSprint = Input.GetAxis("Fire3") == 1f;
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);

        if (inputCrouch)
            isCrouching = !isCrouching;

        // Ground check combinato con piattaforma mobile
        bool isGroundedCustom = cc.isGrounded || (platformFollower != null && platformFollower.IsOnPlatform);

        if (animator != null)
        {
            animator.SetBool("crouch", isCrouching);

            float minimumSpeed = 0.9f;
            bool isActuallyMoving = cc.velocity.magnitude > minimumSpeed;
            animator.SetBool("run", isActuallyMoving && inputVertical != 0f);

            isSprinting = isActuallyMoving && inputSprint;
            animator.SetBool("sprint", isSprinting);

            // Gestione animazione salto
            bool isActuallyInAir = isJumping || (!cc.isGrounded && (platformFollower == null || !platformFollower.IsOnPlatform));
            animator.SetBool("air", isActuallyInAir);
        }

        if (inputJump && isGroundedCustom)
        {
            isJumping = true;
        }

        HeadHittingDetect();
    }

    void FixedUpdate()
    {
        float velocityAdittion = 0;
        if (isSprinting) velocityAdittion = sprintAdittion;
        if (isCrouching) velocityAdittion = -(velocity * 0.5f);

        float directionX = inputHorizontal * (velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (velocity + velocityAdittion) * Time.deltaTime;
        float directionY = 0;

        if (isJumping)
        {
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;
            jumpElapsedTime += Time.deltaTime;

            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        directionY -= gravity * Time.deltaTime;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        forward *= directionZ;
        right *= directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        Vector3 moviment = (Vector3.up * directionY) + (forward + right);
        cc.Move(moviment);

        // Rileva atterraggio e resetta salto
        if (cc.isGrounded && isJumping)
        {
            isJumping = false;
            jumpElapsedTime = 0;
        }
    }

    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }
}

