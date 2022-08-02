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

    // Update is called once per frame
    void Update()
    {
        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    transform.position += transform.forward * movementSpeed * runMultiplier * Time.deltaTime;
                }
                else
                {
                    transform.position += transform.forward * movementSpeed * Time.deltaTime;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    transform.position += -transform.forward * movementSpeed * runMultiplier * Time.deltaTime;
                }
                else
                {
                    transform.position += -transform.forward * movementSpeed * Time.deltaTime;
                }
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
