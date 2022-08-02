using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2;
    [SerializeField]
    private float runMultiplier = 2;
    [SerializeField]
    private float rotationSpeed = 30;

    [HideInInspector]
    public Vector3 NextPosition;

    // Update is called once per frame
    void Update()
    {
        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    NextPosition = transform.forward * movementSpeed * runMultiplier;
                }
                else
                {
                    NextPosition = transform.forward * movementSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    NextPosition = -transform.forward * movementSpeed * runMultiplier;
                }
                else
                {
                    NextPosition = -transform.forward * movementSpeed;
                }
            }
            else
            {
                NextPosition = Vector3.zero;
            }
        }


        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector3 v3 = new Vector3(0.0f, -1f, 0.0f);
                transform.Rotate(v3 * rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Vector3 v3 = new Vector3(0.0f, 1f, 0.0f);
                transform.Rotate(v3 * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
