using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided!!");
        if(collision.gameObject.tag == "Player") {
            PlayerData pd = GameObject.FindFirstObjectByType<PlayerData>();
            if(pd.lives >= 0) {
                pd.lives--;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!!");
        if(other.gameObject.tag == "Player") {
            PlayerData pd = GameObject.FindFirstObjectByType<PlayerData>();
            if(pd.lives >= 0) {
                pd.lives--;
            }
        }
    }
}
