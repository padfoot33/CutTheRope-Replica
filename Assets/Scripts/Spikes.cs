using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Vector3 dir = Vector3.left;
    float speed = 2f;
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);

        if (transform.position.x <= -2)
        {
            dir = Vector3.right;
        }
        else if (transform.position.x >= 2)
        {
            dir = Vector3.left;
        }
    }
}
