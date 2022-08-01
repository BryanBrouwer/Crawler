using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward * movementSpeed * Time.deltaTime;
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