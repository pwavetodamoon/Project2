using UnityEngine;

// create singleton class
namespace Helper
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        [SerializeField] protected bool DontDestroyOnLoad;

        public static T Instance
        {
            get
            {
                // if instance is null
                if (instance == null)
                {
                    // find object of type T
                    instance = FindObjectOfType<T>();
                    // if there is no object of type T
                    if (instance == null)
                    {
                        // create new gameobject
                        var obj = new GameObject();
                        obj.name = typeof(T).Name;
                        // add component of type T
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        // virtual Awake method
        protected virtual void Awake()
        {
            // if instance is null
            if (instance == null)
                // set instance to this
                instance = this as T;
            // if instance is not this
            else if (instance != this)
                // destroy this
                Destroy(gameObject);
            // set this to not be destroyed when reloading scene
            if (DontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}