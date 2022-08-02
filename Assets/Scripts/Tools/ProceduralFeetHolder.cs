using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProceduralFeetHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject feetPrefab;
    [SerializeField]
    private float localFrontZ = 1.73f;
    [SerializeField]
    private float localBackZ = -1.73f;

    [Range(1, 4)]
    public int AmountOfFeetPerSide = 2;


    public List<ProceduralFeetAnimation> ProceduralFeet;

    public void CreateFeet()
    {
        //Destroy all current feet
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
        ProceduralFeet.Clear();

        //Create and place new feet
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
                ProceduralFeet.Add(newFeet.GetComponent<ProceduralFeetAnimation>());
                newFeet.GetComponent<UpdateLineRender>().UpdateRender();
            }
        }
        else
        {
            float localZ = localFrontZ - ((localFrontZ - localBackZ) / 2);
            GameObject newFeet = Instantiate(feetPrefab, transform);
            newFeet.transform.localPosition = new Vector3(newFeet.transform.localPosition.x, newFeet.transform.localPosition.y, localZ);
            ProceduralFeet.Add(newFeet.GetComponent<ProceduralFeetAnimation>());
            newFeet.GetComponent<UpdateLineRender>().UpdateRender();
        }


    }

}
