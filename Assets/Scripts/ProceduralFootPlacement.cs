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
    [Range(0.1f, 40.0f)]
    private float rayDistance = 10.0f;
    [SerializeField]
    [Range(0.1f, 30.0f)]
    private float angleBetweenRays = 10.0f;
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

    public void Awake()
    {
        proceduralFeetAnimation = GetComponentInParent<ProceduralFeetAnimation>();
        currentFootPosition = foot.transform.position;
        NextFootPosition = foot.transform.position;
        FinalFootPosition = foot.transform.position;
        oldFootRotation = foot.transform.rotation;
    }

    // Update is called once per frame
    public void Update()
    {
        //Raycast code
        #region Raycast Region
        if (!IsMoving && !proceduralFeetAnimation.IsOtherFootMoving(this))
        {
            RaycastHit hit;
            bool didRayHit = false;

            float halfAngle = angleBetweenRays / 2;
            Vector3 directionOfFirstRay = Quaternion.AngleAxis(-halfAngle, RaySpawnPoint.transform.right) * -RaySpawnPoint.transform.up;
            Vector3 directionOfSecondRay = Quaternion.AngleAxis(halfAngle, RaySpawnPoint.transform.right) * -RaySpawnPoint.transform.up;

            // Does any ray of the double rays intersect any objects on layerMaskToHit
            if (Physics.Raycast(RaySpawnPoint.position, directionOfFirstRay, out hit, rayDistance, layerMaskToHit))
            {
                didRayHit = true;
#if UNITY_EDITOR
                Debug.DrawRay(RaySpawnPoint.position, directionOfFirstRay * hit.distance, Color.yellow);
                //Debug.Log("First Ray Hit");
#endif
            }//first ray
            else if (Physics.Raycast(RaySpawnPoint.position, directionOfSecondRay, out hit, rayDistance, layerMaskToHit))
            {
                didRayHit = true;
#if UNITY_EDITOR
                Debug.DrawRay(RaySpawnPoint.position, directionOfSecondRay * hit.distance, Color.yellow);
                //Debug.Log("Second Ray Hit");
#endif
            }//second ray
            else
            {
#if UNITY_EDITOR
                Debug.DrawRay(RaySpawnPoint.position, directionOfFirstRay * rayDistance, Color.white);
                Debug.DrawRay(RaySpawnPoint.position, directionOfSecondRay * rayDistance, Color.white);
                //Debug.Log("Did not Hit");
#endif
            }//didnt hit

            if (didRayHit)
            {
                if (Vector3.Distance(currentFootPosition, hit.point) >= distanceBeforeMove)
                {
                    IsMoving = true;
                    FinalFootPosition = hit.point;
                    //For now only adjusting to the normal of the ray hit, attempted to rotate towards moving direction 
                    //as well but I don't have enough time to bugfix that
                    finalFootRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                }
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

    public void SetMinimumDistance(float dis)
    {
        distanceBeforeMove = dis;
    }

    public void MoveToFinalPosition()
    {
        foot.transform.position = FinalFootPosition;
        foot.transform.rotation = finalFootRotation;
    }
}