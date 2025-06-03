using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float fallThreshold = -10f;

    private Rigidbody rb;
    private PlayerData playerData;

    private CameraController cameraController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        cameraController = FindObjectOfType<CameraController>();
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
        if (playerData != null && playerData.lives > 0)
        {
            playerData.lives--;
            Debug.Log("Vita persa! Vite rimanenti: " + playerData.lives);
        }

        // Ferma completamente il movimento del rigidbody
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Respawna il personaggio
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        // Resetta eventuale movimento (es. animazioni, input)
        if (cameraController != null)
        {
            cameraController.ResetTarget(transform); // serve che CameraController abbia questo metodo
        }

        if (playerData != null && playerData.lives <= 0)
        {
            Debug.Log("Game Over!");
            // Disabilita controlli qui se vuoi
        }
    }
}
