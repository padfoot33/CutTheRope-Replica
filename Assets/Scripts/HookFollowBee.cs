using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFollowBee : MonoBehaviour
{
    public Transform bee;

    private void Update()
    {
        transform.position = bee.position + new Vector3(0.06f,-0.35f,0f);
    }
}
