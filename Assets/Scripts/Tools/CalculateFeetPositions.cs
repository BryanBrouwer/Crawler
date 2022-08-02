using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateFeetPositions : MonoBehaviour
{
    public void CalculateCurrentPositions()
    {
        var footPlacements = GetComponentsInChildren<ProceduralFootPlacement>();
        foreach (var item in footPlacements)
        {
            item.Awake();
            item.SetMinimumDistance(0.0f);
            item.Update();
            item.MoveToFinalPosition();
        }
    }
}
