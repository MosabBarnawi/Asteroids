using UnityEngine;

[DisallowMultipleComponent]
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // TODO VALIDATE ON 
    private static bool isEnabled = false;

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

            isEnabled = isEnabled ? true : TryEnable();

            return instance;
        }
    }

    private static bool TryEnable()
    {
        bool isGameObjectEnabled = false;
        bool isSciptEnabled = false;

        if (!instance.gameObject.activeInHierarchy)
        {
            // TODO:: CHECK IF THIS WORKS PROPERLY
            instance.gameObject.SetActive(true);
            isGameObjectEnabled = true;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogWarning($"{ typeof(T) } GameObject is Disabled Enabling Game Object");
            Debug.LogWarning("Enabled Game Object -! Handle This!!");
#endif
        }
        if (!instance.isActiveAndEnabled)
        {
            instance.enabled = true;
            isSciptEnabled = true;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogWarning($"{ typeof(T) } is Disabled Enabling Script");
            Debug.LogWarning("Enabled Script -! Handle This!!");
#endif
        }

        return isGameObjectEnabled && isSciptEnabled;
    }

    private static T instance;

    #region Unity Calls

    protected virtual void OnValidate()
    {
        // TODO:: IMPROVE THIS ON VALIDATE FOR BUILDS
        isEnabled = isActiveAndEnabled;

    }

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
        if (!isthereOtherInstancesOfThis())
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

        // TODO :: HANDLE IF DIABLED

        if (instances.Length == 0 || instance == null) return false;
        else
        {
#if UNITY_EDITOR || DEVELOPEMENT_BUILD 
            Debug.LogError($"Multiple Instances of {typeof(T)}");
#endif
            return true;
        }
    }
    #endregion
}
