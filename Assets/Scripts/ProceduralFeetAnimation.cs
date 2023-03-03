using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralFeetAnimation : MonoBehaviour
{
    //The feet to check values of, think off only moving 1 foot at a time
    private ProceduralFootPlacement[] connectedFeet;

    public ProceduralFootPlacement LeftFoot;
    public ProceduralFootPlacement RightFoot;

    public void Awake()
    {
        connectedFeet = GetComponentsInChildren<ProceduralFootPlacement>();
    }

    public bool IsOtherFootMoving(ProceduralFootPlacement requestingFoot)
    {
        foreach (var item in connectedFeet)
        {
            if (item.IsMoving && item != requestingFoot)
            {
                return true;
            }
        }
        return false;
    }

}
