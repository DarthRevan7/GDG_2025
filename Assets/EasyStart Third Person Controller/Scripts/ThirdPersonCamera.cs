using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Il tuo player
    public float distance = 5f; // Distanza dalla telecamera al player
    public float height = 2f; // Altezza della camera rispetto al player
    public float smoothSpeed = 0.125f; // Velocità di smooth transition della camera

    private Vector3 offset;

    void Start()
    {
        // Calcola l'offset iniziale in base alla posizione iniziale della camera
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // Calcola la posizione finale della telecamera
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Imposta la posizione della telecamera
        transform.position = smoothedPosition;

        // Fai in modo che la telecamera guardi sempre il player
        transform.LookAt(player);
    }
}
