using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlatformFollower : MonoBehaviour
{
    private CharacterController controller;
    private Transform platformTransform;
    private Vector3 lastPlatformPosition;
    private bool isOnPlatform = false;

    public bool IsOnPlatform => isOnPlatform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            platformTransform = hit.collider.transform;
            lastPlatformPosition = platformTransform.position;
            isOnPlatform = true;
        }
    }

    void Update()
    {
        FollowPlatform();
    }

    private void FollowPlatform()
    {
        if (isOnPlatform && platformTransform != null)
        {
            // Calcola delta movimento della piattaforma
            Vector3 platformDelta = platformTransform.position - lastPlatformPosition;

            if (platformDelta != Vector3.zero)
            {
                controller.Move(platformDelta);
            }

            lastPlatformPosition = platformTransform.position;

            // 👇 Verifica se il personaggio è ancora sopra la piattaforma
            Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
            if (!Physics.Raycast(ray, out RaycastHit hit, 0.5f) || !hit.collider.CompareTag("MovingPlatform"))
            {
                platformTransform = null;
                isOnPlatform = false;
            }
        }
    }
}
