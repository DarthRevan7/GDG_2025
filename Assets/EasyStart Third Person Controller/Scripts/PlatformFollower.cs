using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformFollower : MonoBehaviour
{
    private Rigidbody rb;
    private Transform platformTransform;
    private Vector3 lastPlatformPosition;
    private bool isOnPlatform = false;

    public bool IsOnPlatform => isOnPlatform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate; // migliora il movimento con piattaforme
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            platformTransform = collision.collider.transform;
            lastPlatformPosition = platformTransform.position;
            isOnPlatform = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            platformTransform = null;
            isOnPlatform = false;
        }
    }

    void FixedUpdate()
    {
        FollowPlatform();
    }

    private void FollowPlatform()
    {
        if (isOnPlatform && platformTransform != null)
        {
            Vector3 platformDelta = platformTransform.position - lastPlatformPosition;

            if (platformDelta != Vector3.zero)
            {
                rb.MovePosition(rb.position + platformDelta);
            }

            lastPlatformPosition = platformTransform.position;
        }
    }
}
