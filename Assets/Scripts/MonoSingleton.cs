using UnityEngine;
using UnityEngine.PlayerLoop;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            //假设已经挂在场景中
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
        Init();
    }
    
    protected virtual void Init()
    {
        
    }
}