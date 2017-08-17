using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

	Rigidbody2D rb;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void OnEnable()
	{
		float xForce = transform.position.x - GameManager.o.player.position.x;
		print (xForce);
		rb.AddForce (new Vector2 (-xForce * 25, 400));//Random.Range (200, 350)));
//		rb.AddForce (new Vector2 (Random.Range (-100, -200), Random.Range (350, 450)));
		Invoke ("Disable", 5);
	}

	void Disable()
	{
		gameObject.SetActive (false);
	}

//	void OnCollisionEnter2D(Collision2D col)
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name != "bigEnemy")
		{
			CancelInvoke ("Disable");
			Disable ();
//			GameManager.o.GameOver ();
		}

	}


	void Update()
	{
		transform.eulerAngles = new Vector3 (0, 0, -rb.velocity.y * 8);
	}

}
