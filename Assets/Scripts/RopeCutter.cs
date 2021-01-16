using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCutter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Link")
                {
                    Destroy(hit.collider.gameObject);
                    Destroy(hit.transform.parent.parent.gameObject,2f);
                }
                if(hit.collider.tag == "Bubble")
                {
                    hit.collider.GetComponent<Animator>().SetTrigger("BubblePop");
                    GameObject coockie = hit.collider.transform.GetChild(0).gameObject;
                    hit.collider.transform.DetachChildren();
                    coockie.transform.position = hit.collider.transform.position;
                    coockie.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    coockie.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
