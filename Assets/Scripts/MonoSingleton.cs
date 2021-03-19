using UnityEngine;

[DisallowMultipleComponent]
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
#if UNITY_EDITOR || DEVELOPEMENT_BUILD 
                Debug.Log($"Creating A new Singleton of {typeof(T)}");
#endif
                CreateNewInstance();
            }

            return instance;
        }
    }

    private static T instance;

    #region Unity Calls

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = GetComponent<T>();
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    #region Private Methods
    private static void CreateNewInstance()
    {
        if (isthereOtherInstancesOfThis())
        {
            GameObject instancedGameObject = new GameObject();

            instancedGameObject.name = typeof(T).Name;
            instancedGameObject.AddComponent<T>();

            DontDestroyOnLoad(instancedGameObject);
        }
    }

    private static bool isthereOtherInstancesOfThis()
    {
        T[] instances = FindObjectsOfType<T>();

        if (instances.Length == 0 || instance == null) return true;
        else
        {
#if UNITY_EDITOR || DEVELOPEMENT_BUILD 
            Debug.LogError($"Multiple Instances of {typeof(T)}");
#endif
            return false;
        }
    }
    #endregion
}
