using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralFeetHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject feetPrefab;
    [SerializeField]
    private GameObject rayPointsParent;
    [SerializeField]
    private float localFrontZ = 1.73f;
    [SerializeField]
    private float localBackZ = -1.73f;

    [Range(1, 4)]
    public int AmountOfFeetPerSide = 2;


    public List<ProceduralFeetAnimation> ProceduralFeet;

    //Only use/run in editor time.
    public void CreateFeet()
    {
#if UNITY_EDITOR
        //Destroy all current feet
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
        ProceduralFeet.Clear();

        //Create and place new feet // seperated else section for if only 1 foot per side, to save performance on some calculations and if statements. 
        if (AmountOfFeetPerSide > 1)
        {
            float intervalAmount = AmountOfFeetPerSide - 1;
            if (intervalAmount <= 0) intervalAmount = 1;
            float interval = (localFrontZ - localBackZ) / intervalAmount;
            for (int i = 0; i < AmountOfFeetPerSide; i++)
            {
                float localZ = localFrontZ - (interval * i);
                GameObject newFeet = Instantiate(feetPrefab, transform);
                newFeet.transform.localPosition = new Vector3(newFeet.transform.localPosition.x, newFeet.transform.localPosition.y, localZ);
                ProceduralFeetAnimation proceduralFeetAnimation = newFeet.GetComponent<ProceduralFeetAnimation>();
                ProceduralFeet.Add(proceduralFeetAnimation);
                newFeet.GetComponent<UpdateLineRender>().UpdateRender();

                //link ray spawnpoint to correct foot
                GameObject raySpawnPoint = new GameObject("rayPoint");
                raySpawnPoint.transform.parent = rayPointsParent.transform;
                raySpawnPoint.transform.position = proceduralFeetAnimation.LeftFoot.transform.position;
                proceduralFeetAnimation.LeftFoot.RaySpawnPoint = raySpawnPoint.transform;

                raySpawnPoint = new GameObject("rayPoint");
                raySpawnPoint.transform.parent = rayPointsParent.transform;
                raySpawnPoint.transform.position = proceduralFeetAnimation.RightFoot.transform.position;
                proceduralFeetAnimation.RightFoot.RaySpawnPoint = raySpawnPoint.transform;
            }
        }
        else
        {
            float localZ = localFrontZ - ((localFrontZ - localBackZ) / 2);
            GameObject newFeet = Instantiate(feetPrefab, transform);
            newFeet.transform.localPosition = new Vector3(newFeet.transform.localPosition.x, newFeet.transform.localPosition.y, localZ);
            ProceduralFeetAnimation proceduralFeetAnimation = newFeet.GetComponent<ProceduralFeetAnimation>();
            ProceduralFeet.Add(proceduralFeetAnimation);
            newFeet.GetComponent<UpdateLineRender>().UpdateRender();

            //link ray spawnpoint to correct foot
            GameObject raySpawnPoint = new GameObject("rayPoint");
            raySpawnPoint.transform.parent = rayPointsParent.transform;
            raySpawnPoint.transform.position = proceduralFeetAnimation.LeftFoot.transform.position;
            proceduralFeetAnimation.LeftFoot.RaySpawnPoint = raySpawnPoint.transform;

            raySpawnPoint = new GameObject("rayPoint");
            raySpawnPoint.transform.parent = rayPointsParent.transform;
            raySpawnPoint.transform.position = proceduralFeetAnimation.RightFoot.transform.position;
            proceduralFeetAnimation.RightFoot.RaySpawnPoint = raySpawnPoint.transform;
        }
#endif

    }

}
