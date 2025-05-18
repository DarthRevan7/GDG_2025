using UnityEngine;

public class Scream : MonoBehaviour
{

    [SerializeField] private float screamDistanceMax = 100f, screamDistance, screamSpeed=15f;
    [SerializeField] private ParticleSystem explosion;



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
        if (screamDistance >= screamDistanceMax)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (explosion != null)
        {
            ParticleSystem ps = GameObject.Instantiate(explosion, collision.contacts[0].point, Quaternion.identity);
            Destroy(ps.gameObject, ps.main.duration);
            Destroy(gameObject);
        }
    }
}
