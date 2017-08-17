using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

	SpriteRenderer sr;
	public Sprite[] sprites;
	Transform target;

	public float wait = 0.05f;
	float speed = 0.02f;
	bool canMoveTowards;
	int bulletsHit;
	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		StartCoroutine (AnimCoroutine ());
		target = GameManager.o.player;	
		canMoveTowards = true;
		bulletsHit = 0;
		if (transform.position.x < 0) 
		{
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}


	void FixedUpdate()
	{
		if (canMoveTowards)
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed);
	}

	bool isDead = false;
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "player") 
		{
			canMoveTowards = false;
			StopAllCoroutines ();
			sr.sprite = sprites [0];
			GameManager.o.GameOver ();
		}
		else if (col.gameObject.tag == "bullet") 
		{
			bulletsHit++;
			GameManager.o.StartParticle (transform.position);
			if (bulletsHit >= 2 && !isDead) 
			{
				isDead = true;
				GameManager.o.EnemyDie (gameObject);
				Destroy (gameObject);
			}
		}
			
	}
	IEnumerator AnimCoroutine()
	{
		for (int i = 0; i < sprites.Length; i++) 
		{
			sr.sprite = sprites [i];
			yield return new WaitForSeconds (wait);
		}
		StartCoroutine (AnimCoroutine ());
	}
}
