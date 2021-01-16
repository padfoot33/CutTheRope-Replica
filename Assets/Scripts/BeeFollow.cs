using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFollow : MonoBehaviour
{
    public GameObject[] PathNode;
    public float MoveSpeed;

    int CurrentNode;
    float Timer;
    static Vector3 CurrentPositionHolder;

    private Vector2 startPosition;

    void Start()
    {
        CheckNode();
    }

    void CheckNode()
    {
        Timer = 0;
        startPosition = transform.position;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
    }

    void Update()
    {

        Timer += Time.deltaTime * MoveSpeed;

        if (transform.position != CurrentPositionHolder)
        {

            transform.position = Vector3.MoveTowards(startPosition, CurrentPositionHolder, Timer);
        }
        else
        {

            if (CurrentNode < PathNode.Length - 1)
            {
                CurrentNode++;
                CheckNode();
            }
        }
    }
}
