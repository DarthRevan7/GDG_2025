using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.respawnPoint = this.transform;
                Debug.Log("Checkpoint raggiunto: " + transform.position);
            }
        }
    }
}
