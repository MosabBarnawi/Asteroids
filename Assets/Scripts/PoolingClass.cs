using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pooling System Class Which Pools Objects into A Scene
/// </summary>
/// <typeparam name="T">Will Become A Singlton</typeparam>
public class PoolingClass<T> : MonoSingleton<T> where T : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> poolingDictionary;
    private List<GameObject> pooledList;
    private bool isSinglePool = false;
    private Scene pooledScene;

    public void CreatePool(in List<PoolSettings> poolSettings, bool inDifferentScene = false)
    {
        poolingDictionary = new Dictionary<string, List<GameObject>>();

        if (inDifferentScene)
        {
            string name = typeof(T).Name;
            pooledScene = SceneManager.CreateScene(name);
        }

        foreach (PoolSettings item in poolSettings)
        {
            List<GameObject> tempList = new List<GameObject>();

            for (int i = 0; i < item.poolAmount; i++)
            {
                tempList.Add(CreateItemToPoolAndAdd(item));
            }

            poolingDictionary.Add(item.identifier, tempList);
        }
    }

    public void CreatePool(in PoolSettings poolSettings, bool inDifferentScene = false)
    {
        isSinglePool = true;
        pooledList = new List<GameObject>();

        if (inDifferentScene)
        {
            string name = typeof(T).Name;
            pooledScene = SceneManager.CreateScene(name);
        }

        for (int i = 0; i < poolSettings.poolAmount; i++)
        {
            //pooledList.Add(CreateItemToPool(poolSettings.prefab));
            CreateItemToPoolAndAdd(poolSettings);
        }
    }

    private GameObject CreateItemToPoolAndAdd(in PoolSettings poolSettings)
    {
        GameObject go = Instantiate(poolSettings.prefab);
        go.SetActive(false);

        if (pooledScene != null)
        {
            SceneManager.MoveGameObjectToScene(go, pooledScene);
        }
        else
        {
            go.transform.SetParent(transform);
        }

        if (isSinglePool)
        {
            pooledList.Add(go);
        }
        else
        {
            if (poolingDictionary.ContainsKey(poolSettings.identifier))
            {
                poolingDictionary[poolSettings.identifier].Add(go);
            }
        }

        return go;
    }

    public GameObject GetItemFromPool(in PoolSettings poolSettings)
    {
        return isSinglePool ? ReciveItemFromList(poolSettings) : ReciveItemFromDictonary(poolSettings);
    }

    private GameObject ReciveItemFromList(in PoolSettings poolSettings)
    {
        for (int i = 0; i < pooledList.Count; i++)
        {
            if (!pooledList[i].activeInHierarchy)
            {
                return pooledList[i];
            }
        }
        if (!poolSettings.isExpandable) return null;
        else
        {
            if (pooledList.Count < poolSettings.poolCapp)
                return CreateItemToPoolAndAdd(poolSettings);
            else
            {
                return null;
            }
        }
    }

    private GameObject ReciveItemFromDictonary(in PoolSettings poolSettings)
    {
        for (int i = 0; i < poolingDictionary[poolSettings.identifier].Count; i++)
        {
            if (!poolingDictionary[poolSettings.identifier][i].activeInHierarchy)
            {
                return poolingDictionary[poolSettings.identifier][i];
            }
        }
        if (!poolSettings.isExpandable) return null;
        else
        {
            if (poolingDictionary[poolSettings.identifier].Count < poolSettings.poolCapp)
            {

                return CreateItemToPoolAndAdd(poolSettings);
            }
            else
            {
                return null;
            }
        }
    }
}

