using UnityEngine;

public class EntityReferences : MonoBehaviour
{
    /// <summary>
    /// Use it when you know game object have component but you don't know where it is
    /// This method will get component from children or parent to find it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetRef<T>()
    {
        T t = transform.GetComponentInChildren<T>();
        if (t != null)
        {
            return t;
        }
        t = transform.GetComponentInParent<T>();
        return t;
    }
}