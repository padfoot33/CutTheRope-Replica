using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            collision.transform.localPosition = new Vector3(0, 0, 0);
            foreach (HingeJoint2D joint in collision.GetComponents<HingeJoint2D>()) 
                Destroy(joint);
            Destroy(collision.GetComponent<Rigidbody2D>());
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f);
        }
    }
}
