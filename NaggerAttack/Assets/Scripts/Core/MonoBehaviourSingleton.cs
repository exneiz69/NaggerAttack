using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : Singleton where T : MonoBehaviour, ISingleMonoBehaviour
{
    private static T _instance;

    private static readonly object Lock = new object();

    public static T Instance
    {
        get
        {
            if (Quitting)
            {
                Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] " +
                    $"Instance will not be returned because the application is quitting.");
                return null;
            }
            lock (Lock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;
                if (count > 0)
                {
                    if (count > 1)
                    {
                        Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] " +
                            $"There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, " +
                            $"but {count} were found. The first instance found will be used, and all others will be destroyed.");
                        for (int i = 1; i < count; i++)
                        {
                            Destroy(instances[i]);
                        }
                    }
                    return _instance = instances[0];
                }
                else
                {
                    return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}")
                        .AddComponent<T>();
                }
            }
        }
    }
}

public abstract class Singleton : MonoBehaviour
{
    public static bool Quitting { get; private set; }

    #region MonoBehaviour

    private void OnApplicationQuit()
    {
        Quitting = true;
    }

    #endregion
}

public interface ISingleMonoBehaviour { }