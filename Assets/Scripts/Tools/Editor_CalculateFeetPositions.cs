using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(CalculateFeetPositions))]
public class Editor_CalculateFeetPositions : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CalculateFeetPositions myScript = (CalculateFeetPositions)target;
        if (GUILayout.Button("Calculate current feet positions"))
        {
            myScript.CalculateCurrentPositions();
            Editor_UpdateLineRender.UpdateAllLineRenders();
        }
    }

}
