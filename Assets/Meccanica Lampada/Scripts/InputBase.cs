using System.ComponentModel;
using UnityEngine;

public class InputBase : MonoBehaviour
{
    //Movimento
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private KeyCode forward, backward, right, left;

    

        


    //Movimento base
    private void HandleInputOld()
    {
        movementVector = Vector3.zero;
        if(Input.GetKey(right))
        {
            movementVector += new Vector3(1,0,0);
        }
        if(Input.GetKey(forward))
        {
            movementVector += new Vector3(0,0,1);
        }
        if(Input.GetKey(left))
        {
            movementVector -= new Vector3(1,0,0);
        }
        if(Input.GetKey(backward))
        {
            movementVector -= new Vector3(0,0,1);
        }

        movementVector.Normalize();
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputOld();
    }
}
