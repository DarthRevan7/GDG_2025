using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private InputBase inputBase;
    [SerializeField] private Vector3 playerMovement;
    [SerializeField] private Quaternion playerRotation;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputBase = FindAnyObjectByType<InputBase>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement = inputBase.movementVector;
        Vector3 cameraMovement = playerMovement;
        transform.Translate(cameraMovement.normalized);

        // Vector3 lookDir = (transform.position - inputBase.transform.position).normalized;
        // transform.LookAt(lookDir);
    }
}
