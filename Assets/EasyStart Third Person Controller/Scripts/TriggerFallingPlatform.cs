using UnityEngine;

public class TriggerFallingPlatform : MonoBehaviour
{
    public FallingPlatform fallingPlatform;

    void Start()
    {
        fallingPlatform = GetComponentInParent<FallingPlatform>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fallingPlatform.TriggerFall();
            
        }
    }
}
