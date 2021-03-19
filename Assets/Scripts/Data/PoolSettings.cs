using UnityEngine;

[System.Serializable]
public struct PoolSettings
{
    [HideInInspector] public string identifier => prefab.name;
    public GameObject prefab;
    public int poolAmount;
    public int poolCapp;
    public bool isExpandable;
}

