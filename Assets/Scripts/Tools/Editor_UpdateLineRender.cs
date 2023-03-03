using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace Tools
{
    [CustomEditor(typeof(UpdateLineRender))]
    public class Editor_UpdateLineRender : Editor
    {
        [MenuItem("MyTools/Line Renderers/Update All Line Renderers")]
        public static void UpdateAllLineRenders()
        {
            var lineRenders = FindObjectsOfType<UpdateLineRender>();
            foreach (var item in lineRenders)
            {
                item.UpdateRender();
            }
        }

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
}
#endif
