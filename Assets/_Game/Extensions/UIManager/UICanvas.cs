using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;

    private void Awake()
    {
        // filtering
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / Screen.height;
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;

            leftBottom.y = 0f;
            rightTop.y = -100f;

            rect.offsetMin = leftBottom;
            rect.offsetMax = rightTop;
        }
    }

    // call before  active
    public virtual void Setup()
    {

    }

    // call after active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    // close canvas (s)
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }

    // close canvas directly 
    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
