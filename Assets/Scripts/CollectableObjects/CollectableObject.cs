using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectRotation();
    }

    private void ObjectRotation() {
        transform.Rotate(Vector3.up * 1.2f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") {
            GameObject.FindAnyObjectByType<PlayerData>().money++;
            Destroy(gameObject);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            GameObject.FindAnyObjectByType<PlayerData>().money++;
            Destroy(gameObject);
        }
    }
}
