using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    enum MovementSelection
    {
        UP, RIGHT, FORWARD, BACKWARDS, LEFT, DOWN
    }

    [SerializeField] private MovementSelection positiveDirection = MovementSelection.FORWARD;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float actualDistance = 0f, distance = 5f;
    [SerializeField] private float waitingTime = 3f;
    [SerializeField] private bool goForward = true, canTranslate = true;


    // Wait for the next movement; after this negates the value of goForward,
    //  reset the actualDistance and set canTranslate as true.
    IEnumerator WaitingForNextTrip()
    {
        yield return new WaitForSeconds(waitingTime);
        goForward = !goForward;
        actualDistance = 0;
        canTranslate = true;
    }
    // Translates the platform based on the positive direction
    private void TranslateForward()
    {
        // Builds the translation vector
        Vector3 translation = direction * speed * Time.deltaTime;
        // Translates the object if the actualDistance is less then the final translation distance
        if(actualDistance <= distance)
        {
            actualDistance += translation.magnitude;
            transform.Translate(translation);
        }
        // Otherwise stop the object's translation and starts the coroutine for the waiting time
        else
        {
            canTranslate = false;
            StartCoroutine(WaitingForNextTrip());
        }
    }
    // Same as before, just translates on the opposite direction
    private void TranslateBackwards()
    {
        Vector3 translation = -direction * speed * Time.deltaTime;
        if(actualDistance <= distance)
        {
            transform.Translate(translation);
        }
        else
        {
            canTranslate = false;
            StartCoroutine(WaitingForNextTrip());
        }
        actualDistance += translation.magnitude;
    }
    // When the player is on the platform, the parent is changed
    // in order to make the player move with the platform
    // If you delete this, the platform will move and the player will stay still and fall off.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }
    // Serves the opposite purpose: player parent is nullified so it can move independently
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    void Awake()
    {
        // Assign the initial direction for the moving platform
        switch(positiveDirection)
        {
            case MovementSelection.UP:
                direction = Vector3.up;
            break;
            case MovementSelection.RIGHT:
                direction = Vector3.right;
            break;
            case MovementSelection.FORWARD:
                direction = Vector3.forward;
            break;
            case MovementSelection.BACKWARDS:
                direction = Vector3.back;
            break;
            case MovementSelection.LEFT:
                direction = Vector3.left;
            break;
            case MovementSelection.DOWN:
                direction = Vector3.down;
            break;
            default:
                direction = Vector3.forward;
            break;

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(goForward && canTranslate)
        {
            TranslateForward();
        }
        else if(!goForward && canTranslate)
        {
            TranslateBackwards();
        }
    }

    
}
