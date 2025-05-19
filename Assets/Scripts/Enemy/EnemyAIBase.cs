using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 direction = new Vector3();
    [SerializeField] private float aggroRange = 5f;
    [SerializeField] private bool followingPlayer = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerData pd = other.gameObject.GetComponent<PlayerData>();
            //If the player is not invulnerable (Skill Calma)
            if (!pd.invulnerable)
            {
                pd.lives--;
            }
        }
        else if (other.gameObject.GetComponent<Scream>() != null)
        {
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        if (followingPlayer)
        {
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
        }
    }

    private void CalculateDirection()
    {
        direction = transform.position - player.position;
        followingPlayer = direction.magnitude <= aggroRange;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        FollowPlayer();
    }
}
