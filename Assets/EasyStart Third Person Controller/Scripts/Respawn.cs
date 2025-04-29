using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float fallThreshold = -10f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        controller.enabled = false; // Disabilita prima
        transform.position = respawnPoint.position;
        controller.enabled = true; // Riattiva dopo
    }
}
