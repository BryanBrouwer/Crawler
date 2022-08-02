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

    //system variables
    private Quaternion oldFootRotation;
    private Quaternion finalFootRotation;
    private Vector3 currentFootPosition;
    private float fraction = 0;

    [HideInInspector]
    public Transform RaySpawnPoint;
    [HideInInspector]
    public Vector3 NextFootPosition;
    [HideInInspector]
    public Vector3 FinalFootPosition;
    [HideInInspector]
    public bool IsMoving = false;
    //[HideInInspector]
    //public bool IsLeftFoot = false;

    void Awake()
    {
        proceduralFeetAnimation = GetComponentInParent<ProceduralFeetAnimation>();
        currentFootPosition = foot.transform.position;
        NextFootPosition = foot.transform.position;
        FinalFootPosition = foot.transform.position;
        oldFootRotation = foot.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast code
        #region Raycast Region
        if (!IsMoving && !proceduralFeetAnimation.IsOtherFootMoving(this))
        {
            RaycastHit hit;
            // Does the ray intersect any objects on layerMaskToHit
            if (Physics.Raycast(RaySpawnPoint.position, RaySpawnPoint.TransformDirection(Vector3.down), out hit, rayDistance, layerMaskToHit))
            {
#if UNITY_EDITOR
                Debug.DrawRay(RaySpawnPoint.position, RaySpawnPoint.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
#endif
                if (Vector3.Distance(currentFootPosition, hit.point) >= distanceBeforeMove)
                {
                    IsMoving = true;
                    FinalFootPosition = hit.point;
                    finalFootRotation = Quaternion.FromToRotation(foot.transform.up, hit.normal);// * oldFootRotation;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.DrawRay(RaySpawnPoint.position, RaySpawnPoint.TransformDirection(Vector3.down) * rayDistance, Color.white);
                Debug.Log("Did not Hit");
#endif
            }
        }
        #endregion

        //Handle movement
        #region Movement Region
        if (IsMoving)
        {
            fraction += movementAnimationSpeed * Time.deltaTime;
            if (fraction >= 1.0f)
            {
                fraction = 0.0f;
                IsMoving = false;

                //movement
                foot.transform.position = FinalFootPosition;
                NextFootPosition = FinalFootPosition;
                //Update value before next frame to avoid moving foot when moving body
                currentFootPosition = foot.transform.position;

                //rotation
                foot.transform.rotation = finalFootRotation;
                return;
            }
            //movement
            NextFootPosition = Vector3.Lerp(currentFootPosition, FinalFootPosition, fraction);
            NextFootPosition += transform.up * Mathf.Sin(Mathf.PI * 2 * (fraction * 180) / 360);
            foot.transform.position = NextFootPosition;
            //rotation
            foot.transform.rotation = finalFootRotation;
        }
        else
        {
            foot.transform.position = currentFootPosition;
        }
        #endregion
    }
}