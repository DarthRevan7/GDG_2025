using System.ComponentModel;
using UnityEngine;

public class InputBase : MonoBehaviour
{
    //Movimento
    public Vector3 movementVector;
    [SerializeField] private float movementSpeed = 5f, rotationSpeed = 20f;
    [SerializeField] private KeyCode forward = KeyCode.W, backward = KeyCode.S, right = KeyCode.D, left = KeyCode.A;
    [SerializeField] private KeyCode leftRotation = KeyCode.Q, rightRotation = KeyCode.E;

    

        


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
            movementVector += new Vector3(-1,0,0);
        }
        if(Input.GetKey(backward))
        {
            movementVector += new Vector3(0,0,-1);
        }

        movementVector.Normalize();

        if(Input.GetKey(rightRotation)) {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(leftRotation)) {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
        
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
