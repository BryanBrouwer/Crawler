using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UpdateLineRender))]
public class Editor_UpdateLineRender : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UpdateLineRender myScript = (UpdateLineRender)target;
        if (GUILayout.Button("Update Line Renderers"))
        {
            myScript.UpdateRender();
        }
    }

}
