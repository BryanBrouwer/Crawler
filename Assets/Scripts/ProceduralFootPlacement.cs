using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralFootPlacement : MonoBehaviour
{
    //required values
    [SerializeField]
    private GameObject foot;
    private ProceduralFeetAnimation proceduralFeetAnimation;
    //settings
    [SerializeField]
    [Range(0.1f, 20.0f)]
    private float rayDistance = 1.0f;
    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float distanceBeforeMove = 5.0f;
    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float movementAnimationSpeed = 1.0f;
    [SerializeField]
    private LayerMask layerMaskToHit;
    [SerializeField]
    private bool debug = false;

    //system variables
    private Quaternion oldFootRotation;
    private Quaternion finalFootRotation;
    private Vector3 currentFootPosition;
    private float fraction = 0;

    [HideInInspector]
    public Vector3 nextFootPosition;
    [HideInInspector]
    public Vector3 finalFootPosition;
    [HideInInspector]
    public bool isMoving = false;

    void Awake()
    {
        proceduralFeetAnimation = GetComponentInParent<ProceduralFeetAnimation>();
        currentFootPosition = foot.transform.position;
        nextFootPosition = foot.transform.position;
        oldFootRotation = foot.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast code
        #region Raycast Region
        if (!isMoving && !proceduralFeetAnimation.IsOtherFootMoving(this))
        {
            RaycastHit hit;
            // Does the ray intersect any objects on layerMaskToHit
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, layerMaskToHit))
            {
                if (debug)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }

                if (Vector3.Distance(currentFootPosition, hit.point) >= distanceBeforeMove)
                {
                    isMoving = true;
                    finalFootPosition = hit.point;
                    //oldFootRotation = foot.transform.rotation;
                    finalFootRotation = Quaternion.FromToRotation(transform.up, hit.normal) * oldFootRotation;
                }
            }
            else
            {
                if (debug)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDistance, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
        }
        #endregion

        //Handle movement
        #region Movement Region
        if (isMoving)
        {
            fraction += movementAnimationSpeed * Time.deltaTime;
            if (fraction >= 1.0f)
            {
                fraction = 0.0f;
                isMoving = false;

                //movement
                foot.transform.position = finalFootPosition;
                nextFootPosition = finalFootPosition;
                //Update value before next frame to avoid moving foot when moving body
                currentFootPosition = foot.transform.position;

                //rotation
                foot.transform.rotation = finalFootRotation;
                return;
            }
            //movement
            nextFootPosition = Vector3.Lerp(currentFootPosition, finalFootPosition, fraction);
            nextFootPosition += transform.up * Mathf.Sin(Mathf.PI * 2 * (fraction * 180) / 360);
            foot.transform.position = nextFootPosition;
            //rotation
            foot.transform.rotation = finalFootRotation;//Quaternion.Lerp(oldFootRotation, finalFootRotation, fraction);
        }
        else
        {
            foot.transform.position = currentFootPosition;
        }
        #endregion
    }
}