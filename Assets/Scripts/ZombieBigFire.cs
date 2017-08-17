using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBigFire : MonoBehaviour {

	Rigidbody2D rb;
	// Use this for initialization
	void Awake () 
	{
		rb = GetComponent<Rigidbody2D> ();

	}


	void OnEnable()
	{
		float xForce = Mathf.Abs (transform.position.x - GameManager.o.player.position.x);
		rb.AddForce (new Vector2 (-xForce * 40, 300));
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag != "enemy") 
		{
			Destroy (gameObject);
		}
	}
}
