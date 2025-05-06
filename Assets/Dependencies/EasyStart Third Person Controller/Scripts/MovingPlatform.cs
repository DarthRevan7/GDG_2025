using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 direction = Vector3.forward; // Asse Z di default
    public float distance = 5f;
    public float speed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float moveAmount = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + direction.normalized * moveAmount;
    }
}
