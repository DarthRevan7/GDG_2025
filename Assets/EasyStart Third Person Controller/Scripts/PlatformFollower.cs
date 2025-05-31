using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlatformFollower : MonoBehaviour
{
    private CharacterController controller;
    private Transform platformTransform;
    private Vector3 lastPlatformPosition;

    private const float minMoveThreshold = 0.005f;

    public bool IsOnPlatform { get; private set; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            if (platformTransform != hit.collider.transform)
            {
                platformTransform = hit.collider.transform;
                lastPlatformPosition = platformTransform.position;
            }

            IsOnPlatform = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("MovingPlatform"))
        {
            platformTransform = null;
            IsOnPlatform = false;
        }
    }

    void LateUpdate()
    {
        if (platformTransform != null)
        {
            Vector3 platformDelta = platformTransform.position - lastPlatformPosition;

            if (platformDelta.magnitude > minMoveThreshold)
            {
                controller.Move(platformDelta);
            }

            lastPlatformPosition = platformTransform.position;
        }
    }
}
