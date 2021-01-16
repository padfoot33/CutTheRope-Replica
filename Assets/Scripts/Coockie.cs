using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Coockie : MonoBehaviour
{
    public CanvasScript CanvasObject;
	public float distanceFromChainEnd = 0.6f;
    public GameObject[] StarHolders;
    public GameObject Frog;
    public float bounce = 200f;

    private void Start()
    {
        PlayerPrefs.SetInt("DisplayStars", 0);
    }

    public void ConnectRopeEnd(Rigidbody2D endRB)
	{
		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = endRB;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            CanvasObject.GameOverPanel.SetActive(true);
            Destroy(gameObject, 1f);
        }
        if (collision.CompareTag("Star"))
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            PlayerPrefs.SetInt("DisplayStars",PlayerPrefs.GetInt("DisplayStars") + 1);
            collision.GetComponentInChildren<ParticleSystem>().Play();
            StartCoroutine(Translate(collision.gameObject, StarHolders[StarHolders.Length - 1]));
            StarHolders = StarHolders.Take(StarHolders.Length - 1).ToArray();
        }
        if (collision.CompareTag("FrogBoundry"))
        {
            Frog.GetComponent<Animator>().SetBool("EatingEnter", true);
        }
        if (collision.CompareTag("Frog"))
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.position = collision.transform.position;
            StartCoroutine(LevelComplete());
        }
        if (collision.CompareTag("Hook"))
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            collision.GetComponentInChildren<Rope>().coockie = this;
            collision.GetComponentInChildren<Rope>().GenerateRope();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FrogBoundry"))
        {
            Frog.GetComponent<Animator>().SetBool("EatingEnter", false);
            Frog.GetComponent<Animator>().SetBool("EatingStay", true);
            Frog.GetComponent<Animator>().SetBool("EatingExit", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FrogBoundry"))
        {
            Frog.GetComponent<Animator>().SetBool("EatingEnter", false);
            Frog.GetComponent<Animator>().SetBool("EatingStay", false);
            Frog.GetComponent<Animator>().SetBool("EatingExit", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RubberBounce"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Bounce");
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().AddForce(collision.contacts[0].normal * bounce);
        }
    }
    IEnumerator Translate(GameObject obj, GameObject target)
    {
        while (obj.transform.position != target.transform.position)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, 
                target.transform.position, 1.5f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        CanvasObject.OnLevelComplete();
        CanvasObject.LevelCompletePanel.SetActive(true);
    }
}
