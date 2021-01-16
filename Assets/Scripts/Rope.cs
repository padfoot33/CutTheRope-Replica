using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject linkPrefab;
    public Coockie coockie;
    public int links = 7;

    private bool gen = true;

    private void Start()
    {
        if(coockie)
            GenerateRope();
    }

    

    public void GenerateRope()
    {
        Rigidbody2D previousRB = hook;
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            if (i < links - 1)
            {
                previousRB = link.GetComponent<Rigidbody2D>();
            }
            else
            {
                coockie.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
            }

        }
    }
}
