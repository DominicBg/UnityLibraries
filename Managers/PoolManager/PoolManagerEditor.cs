using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//MUST BE IN EDITOR FOLDER TO COMPILE

[CustomEditor(typeof(PoolManager))]
public class PoolManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PoolManager poolManager = (PoolManager)target;
        if (GUILayout.Button("Generate Const File"))
        {
            poolManager.GenerateConstFile();
        }
    }
}
