using UnityEngine;

public class Scream : MonoBehaviour
{

    [SerializeField] private float screamDistanceMax, screamDistance, screamSpeed;



    void Awake()
    {
        // screamDistanceMax = transform.parent.GetComponent<ScreamSkill>().screamDistanceMax;
        // screamSpeed = transform.parent.GetComponent<ScreamSkill>().screamSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * screamSpeed * Time.deltaTime);
        screamDistance += (transform.forward * screamSpeed * Time.deltaTime).magnitude;
        if(screamDistance >= screamDistanceMax)
        {
            Destroy(gameObject);
        }
    }
}
