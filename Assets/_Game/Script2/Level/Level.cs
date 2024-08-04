using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Transform playerStartPoint;

    public List<Transform> enemyStartPoint = new List<Transform>();

    public Transform winPoint;

    public List<Floor> floors = new List<Floor>();
}
