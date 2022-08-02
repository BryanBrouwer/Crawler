using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBodyPositioning : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 3.0f)]
    private float heightOffset = 2.0f;
    [SerializeField]
    [Range(0.0f, 3.0f)]
    private float rotationMultiplier = 2.0f;
    [SerializeField]
    private bool excludeMovingFeet = true;
    [SerializeField]
    [Range(1.0f, 100.0f)]
    private float lerpMultiplier = 10f;

    //system variables
    private List<ProceduralFeetAnimation> proceduralFeet;
    private ProceduralFootPlacement[] proceduralFootPlacements;
    private Transform controllerTransform;

    private void Awake()
    {
        proceduralFootPlacements = transform.parent.GetComponentsInChildren<ProceduralFootPlacement>();
        proceduralFeet = transform.parent.GetComponentInChildren<ProceduralFeetHolder>().ProceduralFeet;
        controllerTransform = transform.root;
    }

    // Update is called once per frame
    void Update()
    {
        //Height adjustment
        float averageHeight = 0.0f;
        if (excludeMovingFeet)
        {
            for (int i = 0; i < proceduralFootPlacements.Length; i++)
            {
                if (!proceduralFootPlacements[i].IsMoving)
                {
                    averageHeight += proceduralFootPlacements[i].NextFootPosition.y;
                }
                else
                {
                    averageHeight += proceduralFootPlacements[i].FinalFootPosition.y;
                }
            }
        }
        else
        {
            for (int i = 0; i < proceduralFootPlacements.Length; i++)
            {
                averageHeight += proceduralFootPlacements[i].NextFootPosition.y;
            }
        }

        averageHeight = (averageHeight / proceduralFootPlacements.Length) + heightOffset;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, averageHeight, transform.position.z), lerpMultiplier * Time.deltaTime);

        //Rotation
        /* attempt 1
        //left right difference only upper front and final last
        bool leftIsHigher = false;
        float leftAverageHeight = 0f;
        for (int i = 0; i < proceduralFeet.Count; i += proceduralFeet.Count - 1)
        {
            leftAverageHeight += proceduralFeet[i].LeftFoot.FinalFootPosition.y;
        }
        leftAverageHeight /= 2;
        float rightAverageHeight = 0f;
        for (int i = 0; i < proceduralFeet.Count; i += proceduralFeet.Count - 1)
        {
            rightAverageHeight += proceduralFeet[i].RightFoot.FinalFootPosition.y;
        }
        rightAverageHeight /= 2;

        if (leftAverageHeight > rightAverageHeight) leftIsHigher = true;
        //else if (leftAverageHeight == rightAverageHeight) return;

        float delta = 0;

        delta = leftIsHigher ? leftAverageHeight - rightAverageHeight : rightAverageHeight - leftAverageHeight;
        float rotationDegrees = delta * rotationMultiplier * Time.deltaTime;

        //If right leg is higher, invert the rotation to a negative value
        rotationDegrees = leftIsHigher ? rotationDegrees : rotationDegrees * -1;
        //Rotate degrees around Z axis
        controllerTransform.Rotate(Vector3.forward * rotationDegrees);
        */
        //attempt 2
        Vector3 localForward = proceduralFeet[0].LeftFoot.FinalFootPosition - proceduralFeet[proceduralFeet.Count - 1].LeftFoot.FinalFootPosition;
        Vector3 localRight = proceduralFeet[0].RightFoot.FinalFootPosition - proceduralFeet[0].LeftFoot.FinalFootPosition;
        Vector3 localUp = Vector3.Cross(localForward, localRight);

        controllerTransform.rotation = Quaternion.Lerp(controllerTransform.rotation, Quaternion.LookRotation(localForward, localUp), rotationMultiplier * Time.deltaTime);
    }
}
