using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => GetInstance();

    private static T GetInstance()
    {
        if (!instance)
        {
            instance = FindAnyObjectByType<T>();
            if (!instance)
            {
                new GameObject(typeof(T).ToString(), typeof(T));
            } else if (instance.gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(instance.gameObject);
            }
        }

        return instance;
    }

    private void Awake()
    {
        GetInstance();

        if (instance && instance != this)
        {
            Debug.LogWarning($"Destroying duplicate {typeof(T)} component.");
            Destroy(this);
        }
    }
}
