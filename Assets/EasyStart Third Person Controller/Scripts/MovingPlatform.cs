using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    public enum MovementType
    {
        PingPong,
        Loop,
        Path
    }

    public MovementType movementType = MovementType.PingPong;

    public Vector3 direction = Vector3.forward;
    public float distance = 5f;
    public float speed = 2f;

    [Tooltip("Usa solo se movementType è Path")]
    public Transform[] pathPoints;

    private Vector3 startPosition;
    private int currentPathIndex = 0;

    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Importante!

        if (movementType == MovementType.Path && (pathPoints == null || pathPoints.Length == 0))
        {
            Debug.LogWarning("Path movement selezionato ma pathPoints non assegnati o vuoti.");
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = transform.position;

        switch (movementType)
        {
            case MovementType.PingPong:
                {
                    float moveAmount = Mathf.PingPong(Time.time * speed, distance);
                    targetPosition = startPosition + direction.normalized * moveAmount;
                    break;
                }
            case MovementType.Loop:
                {
                    float moveAmount = (Time.time * speed) % distance;
                    targetPosition = startPosition + direction.normalized * moveAmount;
                    break;
                }
            case MovementType.Path:
                {
                    if (pathPoints == null || pathPoints.Length == 0) return;

                    Vector3 target = pathPoints[currentPathIndex].position;
                    targetPosition = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

                    if (Vector3.Distance(transform.position, target) < 0.01f)
                    {
                        currentPathIndex++;
                        if (currentPathIndex >= pathPoints.Length)
                            currentPathIndex = 0;
                    }
                    break;
                }
        }

        rb.MovePosition(targetPosition);
    }
}
