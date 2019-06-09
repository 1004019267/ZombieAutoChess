using UnityEngine;
/// <summary>
/// µ¥Àý
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;


    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = _instance.GetType().ToString();
                }
            }
            return _instance;
        }
    }

    public static T getMe()
    {
        return Instance;
    }

    protected virtual void Awake ()
		{
			_instance = this as T;			
		}
	}

