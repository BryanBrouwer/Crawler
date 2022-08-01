using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBodyPositioning : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 3.0f)]
    private float heightOffset = 2.0f;
    [SerializeField]
    private bool excludeMovingFeet = true;
    [SerializeField]
    [Range(1.0f, 100.0f)]
    private float lerpMultiplier = 10f;

    //system variables
    private ProceduralFootPlacement[] proceduralFeet;

    private void Awake()
    {
        proceduralFeet = transform.parent.GetComponentsInChildren<ProceduralFootPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        float averageHeight = 0.0f;
        if (excludeMovingFeet)
        {
            for (int i = 0; i < proceduralFeet.Length; i++)
            {
                if (!proceduralFeet[i].isMoving)
                {
                    averageHeight += proceduralFeet[i].nextFootPosition.y;
                }
                else
                {
                    averageHeight += proceduralFeet[i].finalFootPosition.y;
                }
            }
        }
        else
        {
            for (int i = 0; i < proceduralFeet.Length; i++)
            {
                averageHeight += proceduralFeet[i].nextFootPosition.y;
            }
        }

        averageHeight = (averageHeight / proceduralFeet.Length) + heightOffset;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, averageHeight, transform.position.z), lerpMultiplier * Time.deltaTime);
        //transform.position = (transform.position + averagePosition) / 2;
    }
}
