using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(breadCrumbManager))]
public class breadCrumbInspectorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        breadCrumbManager myScript = (breadCrumbManager)target;
        if(GUILayout.Button("Create Breadcrumb"))
        {
            myScript.spawnBreadcrumb();
        }
    }
}
