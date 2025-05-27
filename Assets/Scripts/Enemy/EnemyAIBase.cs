using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 direction = new Vector3(), startingPosition;
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
            //direction.y = transform.position.y;
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
        }
        if (!followingPlayer && Vector3.Distance(transform.position, startingPosition) >= 0.5f)
        {
            transform.Translate((startingPosition - transform.position).normalized * movementSpeed * Time.deltaTime);
        }
    }

    private void CalculateDirection()
    {
        direction = player.position - transform.position;
        direction.y = 0.0f;
        followingPlayer = direction.magnitude <= aggroRange;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        FollowPlayer();
    }
}
