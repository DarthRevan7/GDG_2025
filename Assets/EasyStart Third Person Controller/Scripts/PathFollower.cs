using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] pathPoints;
    public float speed = 2f;
    public bool drawPathGizmos = true;

    private int currentPointIndex = 0;
    private bool isMoving = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!isMoving || pathPoints.Length == 0) return;

        Transform targetPoint = pathPoints[currentPointIndex];
        Vector3 direction = targetPoint.position - transform.position;

        // Movimento preciso
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Quando raggiungiamo il punto
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Length)
            {
                isMoving = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (drawPathGizmos && pathPoints != null && pathPoints.Length > 1)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                if (pathPoints[i] != null && pathPoints[i + 1] != null)
                {
                    Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
                    Gizmos.DrawSphere(pathPoints[i].position, 0.1f);
                }
            }

            // Punto finale
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pathPoints[^1].position, 0.15f);
        }
    }
}
