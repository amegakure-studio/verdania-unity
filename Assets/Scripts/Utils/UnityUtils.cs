
using UnityEngine;

public static class UnityUtils
{
    public static T FindOrCreateComponent<T>() where T : Component
    {
        T component = GameObject.FindObjectOfType<T>();
        if (component == null)
        {
            GameObject newGameObject = new (typeof(T).Name);
            component = newGameObject.AddComponent<T>();
        }
        return component;
    }
}