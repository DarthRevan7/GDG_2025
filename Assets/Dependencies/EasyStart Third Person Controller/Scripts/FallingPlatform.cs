using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float delayBeforeFall = 0.5f;
    public float destroyDelayAfterFall = 2f; // ⬅️ Nuovo parametro per quanto tempo aspettare prima di distruggere
    private Rigidbody rb;
    private bool hasTriggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void TriggerFall()
    {
        if (!hasTriggered)
        {
            hasTriggered = true;
            Invoke("DropPlatform", delayBeforeFall);
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
        Destroy(gameObject, destroyDelayAfterFall); // ⬅️ Distruggi la piattaforma dopo X secondi
    }
}
