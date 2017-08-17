using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{

	SpriteRenderer sr;
	public Sprite[] sprites;

	public Transform target;
	Transform firePos;

	public GameObject bigEnemyFire;
	public float wait = 0.05f;
	int bulletHits;
	int maxBulletHits;
	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		firePos = transform.GetChild (0);
		StartCoroutine (AnimCoroutine());
		bulletHits = 0;
		maxBulletHits = 100;
		Invoke ("Fire", 5);
	}
	void Fire()
	{
		if (!GameManager.o.isGameOver) 
		{
			GameObject obj = Instantiate (bigEnemyFire);
			obj.transform.position = firePos.position;
			obj.SetActive (true);
		}

		if (!GameManager.o.isGameOver)
			Invoke ("Fire", 5);


	}
	bool moveTowards = true;
	void FixedUpdate()
	{
		if (moveTowards)
		{
			transform.position = Vector3.MoveTowards (transform.position, target.position, 0.01f);
			if (transform.position == target.position) 
			{
				moveTowards = false;
//				StopAllCoroutines ();
				sr.sprite = sprites [0];
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

	void OnCollisionEnter2D(Collision2D col)
//	void OnCollisionEnter2D(Collision2D col)

	{
		if (col.gameObject.tag == "bullet") 
		{
			bulletHits++;
			if (bulletHits < maxBulletHits) 
			{
				GameManager.o.StartParticle (col.contacts [0].point);
//				print ("hit");
			}
			else
			{
				GameManager.o.GameEnd ();
			}
		}
		else if (col.gameObject.tag == "player") 
		{
			moveTowards = false;
			print ("player");
			GameManager.o.GameOver ();
		}
	}
}
