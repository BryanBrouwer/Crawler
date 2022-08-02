using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ProceduralFeetHolder))]
public class Editor_ProceduralFeetHolder : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ProceduralFeetHolder myScript = (ProceduralFeetHolder)target;
        if (GUILayout.Button("Create feet"))
        {
            string path = "";
            //Get prefab assetPath from prefabStage (When having the prefab open in editor)
            var prefabStage = PrefabStageUtility.GetPrefabStage(myScript.gameObject);
            if (prefabStage != null)
            {
                path = prefabStage.assetPath;
            }
            else
            {
                var test = PrefabUtility.GetCorrespondingObjectFromSource(target);
                path = AssetDatabase.GetAssetPath(test);
            }

            // Load the contents of the Prefab Asset.
            GameObject contentsRoot = PrefabUtility.LoadPrefabContents(path);

            // Modify Prefab contents.
            contentsRoot.GetComponentInChildren<ProceduralFeetHolder>().AmountOfFeetPerSide = myScript.AmountOfFeetPerSide;
            contentsRoot.GetComponentInChildren<ProceduralFeetHolder>().CreateFeet();

            // Save contents back to Prefab Asset and unload contents.
            PrefabUtility.SaveAsPrefabAsset(contentsRoot, path);
            PrefabUtility.UnloadPrefabContents(contentsRoot);
            Editor_UpdateLineRender.UpdateAllLineRenders();
        }
    }
}
