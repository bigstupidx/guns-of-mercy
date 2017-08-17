using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFire : MonoBehaviour {

	Rigidbody2D rb;
	// Use this for initialization
	void Awake () 
	{
		rb = GetComponent<Rigidbody2D> ();
		
	}

	void OnEnable()
	{
		if (transform.position.x - GameManager.o.player.position.x > 0) 
		{
			rb.AddForce (new Vector2 (-200, 200));
		}
		else 
		{
			rb.AddForce (new Vector2 (200, 200));
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag != "enemy") 
		{
			gameObject.SetActive (false);
		}
	}
}
