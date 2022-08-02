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
    private Transform controllerTransform;

    private void Awake()
    {
        proceduralFeet = transform.parent.GetComponentInChildren<ProceduralFeetHolder>().ProceduralFeet;
        controllerTransform = transform.root;
    }

    // Update is called once per frame
    void Update()
    {
        ////movement of body
        transform.position = Vector3.Lerp(transform.position, ((proceduralFeet[0].RightFoot.FinalFootPosition
            + proceduralFeet[proceduralFeet.Count - 1].LeftFoot.FinalFootPosition) * 0.5f) + transform.up * heightOffset, lerpMultiplier * Time.deltaTime);

        //Rotation of entire controller
        Vector3 localForward = proceduralFeet[0].LeftFoot.FinalFootPosition - proceduralFeet[proceduralFeet.Count - 1].LeftFoot.FinalFootPosition;
        Vector3 localRight = proceduralFeet[0].RightFoot.FinalFootPosition - proceduralFeet[0].LeftFoot.FinalFootPosition;
        Vector3 localUp = Vector3.Cross(localForward, localRight);

        controllerTransform.rotation = Quaternion.Lerp(controllerTransform.rotation, Quaternion.LookRotation(localForward, localUp), rotationMultiplier * Time.deltaTime);
    }
}
