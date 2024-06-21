using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private readonly Dictionary<System.Type, Lazy<UICanvas>> canvasPrefabs = new Dictionary<System.Type, Lazy<UICanvas>>();
    private readonly Dictionary<System.Type, UICanvas> canvasActives = new Dictionary<System.Type, UICanvas>();
    [SerializeField] private Transform uiParent;

    private void Awake()
    {
        // Load ui prefab to resources
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI/");
        foreach (UICanvas prefab in prefabs)
        {
            canvasPrefabs.Add(prefab.GetType(), new Lazy<UICanvas>(() => prefab));
        }
    }

    // open canvas
    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();

        canvas.Setup();
        canvas.Open();

        return canvas;
    }

    // close canvas (s)
    public void CloseUI<T>(float time) where T : UICanvas
    {
        if (TryGetActiveCanvas<T>(out UICanvas canvas))
        {
            canvas.Close(time);
        }
    }

    // close canvas directly 
    public void CloseUIDirectly<T>() where T : UICanvas
    {
        if (TryGetActiveCanvas<T>(out UICanvas canvas))
        {
            canvas.CloseDirectly();
        }
    }

    // check canvas open
    public bool IsLoaded<T>() where T : UICanvas
    {
        return canvasActives.TryGetValue(typeof(T), out UICanvas canvas) && canvas != null;
    }

    // check active canvas 
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }

    // take active canvas
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canvas = Instantiate(prefab, uiParent);
            canvasActives[typeof(T)] = canvas;
        }

        return canvasActives[typeof(T)] as T;
    }

    // get prefab
    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)].Value as T;
    }

    // close all
    public void CloseAll()
    {
        foreach (var canvas in canvasActives.Values)
        {
            if (canvas != null && canvas.gameObject.activeSelf)
            {
                canvas.Close(0);
            }
        }
    }

    private bool TryGetActiveCanvas<T>(out UICanvas canvas) where T : UICanvas
    {
        return canvasActives.TryGetValue(typeof(T), out canvas);
    }
}