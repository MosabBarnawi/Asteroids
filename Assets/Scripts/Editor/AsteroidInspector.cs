using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Asteroids))]
public class AsteroidInspector
    : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Asteroids astoroid = (Asteroids)target;

        GUILayout.Space(20);

        if(GUILayout.Button("Open Astroid Settings"))
        {
            AssetDatabase.OpenAsset(astoroid.AstroidScriptable);
        }

    }
}
