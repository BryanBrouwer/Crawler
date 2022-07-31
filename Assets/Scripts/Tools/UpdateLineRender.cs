using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct LineConnections
{
    public List<GameObject> ConnectPoints;
}

public class UpdateLineRender : MonoBehaviour
{
    [SerializeField]
    private List<LineConnections> lineConnectPoints;
    [SerializeField]
    private List<LineRenderer> lineRenderers;

    public void UpdateRender()
    {
        for (int i = 0; i < lineRenderers.Count; i++)
        {
            for (int x = 0; x < lineConnectPoints.Count; x++)
            {
                lineRenderers[i].SetPosition(x, lineConnectPoints[i].ConnectPoints[x].transform.position);
            }
        }
    }

    private void Update()
    {
        UpdateRender();
    }
}
