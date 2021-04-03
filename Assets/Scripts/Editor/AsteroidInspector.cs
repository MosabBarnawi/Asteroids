using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Asteroid))]
public class AsteroidInspector
    : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Asteroid astoroid = (Asteroid)target;

        GUILayout.Space(20);

        if(GUILayout.Button("Open Astroid Settings"))
        {
            AssetDatabase.OpenAsset(astoroid.AstroidScriptable);
        }

    }
}
