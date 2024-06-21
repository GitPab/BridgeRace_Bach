using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMono : MonoBehaviour
{
    private Transform tf;

    protected Transform Tf
    {
        get {
            if (tf == null)
            {
                tf = transform;
            }
            return tf; 
        }
    }
}
