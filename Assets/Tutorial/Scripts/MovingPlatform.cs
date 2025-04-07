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



    IEnumerator WaitingForNextTrip()
    {
        yield return new WaitForSeconds(waitingTime);
        goForward = !goForward;
        actualDistance = 0;
        canTranslate = true;
    }

    private void TranslateForward()
    {
        Vector3 translation = direction * speed * Time.deltaTime;
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    void Awake()
    {
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
