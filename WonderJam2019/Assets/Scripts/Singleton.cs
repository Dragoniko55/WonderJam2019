using UnityEngine;

public class Singleton<TSingleton> : MonoBehaviour where TSingleton : Component
{
    private static bool _shuttingDown = false;
    private static object _lock = new object();
    private static TSingleton _instance;

    public static TSingleton Instance
    {
        get
        {
            if (_shuttingDown)
            {
                Debug.LogWarning("Attempting to retrieve singleton object after application shutdown. Returning default.");
                return default;
            }

            EnsureCreated();
            return _instance;
        }
    }

    public static void EnsureCreated()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TSingleton>();

                if (_instance == null)
                {
                    var singleton = new GameObject();
                    _instance = singleton.AddComponent<TSingleton>();
                    singleton.name = $"{typeof(TSingleton).Name} (Singleton)";

                    //DontDestroyOnLoad(singleton);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        _shuttingDown = true;
    }

    private void OnDestroy()
    {
        _shuttingDown = true;
    }
}