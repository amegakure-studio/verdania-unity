using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dojo;
using UnityEngine;

public class ModelGenerator : MonoBehaviour
{
    void Awake()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(ModelInstance));
        var allItems = assembly.GetTypes()
            .Where(t => typeof(ModelInstance).IsAssignableFrom(t) && t.IsAbstract == false);

        GameObject go = new GameObject("--Models--");
        
        foreach (System.Type script in allItems)
        {
            Debug.Log(script.Name);
            // ModelInstance model = Activator.CreateInstance(script) as ModelInstance;

            // go.AddComponent(model.GetType());
            go.AddComponent(script);
        }
    }
}
