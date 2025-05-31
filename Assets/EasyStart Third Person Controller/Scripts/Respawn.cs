using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float fallThreshold = -10f;

    private CharacterController controller;
    private PlayerData playerData; // <--- aggiunto

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerData = GetComponent<PlayerData>(); // <--- aggiunto
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
        // Diminuire la vita solo se non è già 0
        if (playerData != null && playerData.lives > 0)
        {
            playerData.lives--;
            Debug.Log("Vita persa! Vite rimanenti: " + playerData.lives);
        }

        // Respawn del personaggio
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;

        // Se vuoi, qui puoi anche controllare se il player ha finito le vite:
        if (playerData != null && playerData.lives <= 0)
        {
            Debug.Log("Game Over!");
            // Puoi aggiungere qui una schermata di game over, disabilitare controlli, ecc.
        }
    }
}
