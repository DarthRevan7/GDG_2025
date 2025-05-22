using UnityEngine;

public class CollectableObject : MonoBehaviour
{

    public enum ObjectType
    {
        MONEY, LIFE
    }

    public ObjectType objectType;


    // Update is called once per frame
    void Update()
    {
        ObjectRotation();
    }

    private void ObjectRotation() {
        transform.RotateAround(transform.position, Vector3.up, 1.2f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") {
            if (objectType == ObjectType.MONEY)
            {
                GameObject.FindAnyObjectByType<PlayerData>().money++;
            }
            else if (objectType == ObjectType.LIFE)
            {
                GameObject.FindAnyObjectByType<PlayerData>().lives++;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (objectType == ObjectType.MONEY)
            {
                GameObject.FindAnyObjectByType<PlayerData>().money++;
            }
            else if (objectType == ObjectType.LIFE)
            {
                GameObject.FindAnyObjectByType<PlayerData>().lives++;
            }
            Destroy(gameObject);
    }
}
