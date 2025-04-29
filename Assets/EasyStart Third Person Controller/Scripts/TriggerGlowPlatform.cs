using UnityEngine;

public class TriggerGlowPlatform : MonoBehaviour
{
    public PlatformLightUp targetPlatform;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetPlatform != null)
        {
            targetPlatform.Glow();
        }
    }
}
