using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        CalculateOffset();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    private void CalculateOffset()
    {
        offset = transform.position - player.position;
    }

}
