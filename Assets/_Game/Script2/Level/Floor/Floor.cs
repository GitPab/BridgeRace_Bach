using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Floor : MonoBehaviour
{
    [SerializeField] private Brick brickFloor;
    [SerializeField] private Transform brickPrefabHolder;
    [SerializeField] private float spacing = 2.0f;
    public List<Brick> brickInFloor = new List<Brick>();

    [SerializeField] private Transform brickSpawnPosition;

    public List<int> colorNumber = new List<int>();

    private int[,] BrickCoordinate = new int[12, 12];

    private void Start()
    {
        LoadMap();
        InvokeRepeating(nameof(RestoreMap), 0f, 3f);
    }

    private void LoadMap()
    {
        for (int i = 0; i < BrickCoordinate.GetLength(0); i++)
        {
            for (int j = 0; j < BrickCoordinate.GetLength(1); j++)
            {
                brickFloor.ChangeColor(ColorType.Transparent);
                InstantiateBrick(i, j);
            }
        }

        // Activate all bricks after instantiation

        foreach (Brick brick in brickInFloor)

        {

            brick.gameObject.SetActive(true);

        }
    }

    public void RestoreMap()
    {
        if (brickInFloor.Count > 0)
        {
            foreach (Brick brick in brickInFloor)
            {
                int rand = Random.Range(1, 9);
                if (colorNumber.Contains(rand))
                {
                    brick.ChangeColor((ColorType)rand);
                }
            }
        }
    }

    private void InstantiateBrick(int i, int j)
    {
        Vector3 position = brickSpawnPosition.position + new Vector3(i * spacing, 0.1f, -j * spacing);
        Brick brickPrefab = Instantiate(brickFloor, position, Quaternion.identity);
        brickPrefab.gameObject.SetActive(false); // Deactivate the brick initially
        brickPrefab.transform.parent = brickPrefabHolder;
        brickInFloor.Add(brickPrefab);
    }

    public void ClearBrickInStage()
    {
        foreach (Brick brick in brickInFloor)
        {
            Destroy(brick.gameObject);
        }
        brickInFloor.Clear();
    }

    public Vector3 SeekBrick(ColorType color)
    {
        if (brickInFloor.Count > 0)
        {
            Transform brickNearest = brickInFloor[0].transform;
            float disMin = Vector3.Distance(transform.position, brickNearest.position);
            foreach (Brick brick in brickInFloor)
            {
                if (brick.color == color)
                {
                    float dis = Vector3.Distance(transform.position, brick.transform.position);
                    if (dis < disMin)
                    {
                        disMin = dis;
                    }
                }
            }
            foreach (Brick brick in brickInFloor)
            {
                if (brick.color == color)
                {
                    float dis = Vector3.Distance(transform.position, brick.transform.position);
                    if (Mathf.Abs(dis - disMin) <= 0.1f)
                    {
                        return brick.transform.position;
                    }
                }
            }
        }
        return Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.PLAYER_TAG) || other.gameObject.CompareTag(TagName.ENEMY_TAG))
        {
            ColorType color = other.gameObject.GetComponent<Character>().color;
            colorNumber.Add((int)color);
        }
    }
}