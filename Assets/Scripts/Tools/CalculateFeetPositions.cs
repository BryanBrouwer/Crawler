using UnityEngine;

namespace Tools
{
    public class CalculateFeetPositions : MonoBehaviour
    {
        public void CalculateCurrentPositions()
        {
            //set variables from on awake for feet animation components
            var feetAnimations = GetComponentsInChildren<ProceduralFeetAnimation>();
            foreach (var item in feetAnimations)
            {
                item.Awake();
            }

            //call awake for variable setting, and then call positioning functions
            var footPlacements = GetComponentsInChildren<ProceduralFootPlacement>();
            foreach (var item in footPlacements)
            {
                item.Awake();
                item.SetMinimumDistance(0.0f);
                item.RayCastFootPlacement();
                item.MoveToFinalPosition();
            }
        }
    }
}
