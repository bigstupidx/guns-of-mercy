using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieBig : MonoBehaviour {


	Animator animator;
	public Transform target;
	Transform firePos;
	public Image healthBar;
	public GameObject bigEnemyFire;
	public int bulletHits;
	int maxBulletHits;
	bool moveTowards;
	Vector3 startPos;


	// Use this for initialization
	void Start () 
	{
		startPos = transform.position;
		animator = GetComponent<Animator> ();
		firePos = transform.GetChild (0);
		bulletHits = 0;
		maxBulletHits = 100;
		moveTowards = true;
//		Invoke ("Fire", 5);
	}

	void OnEnable()
	{
		healthBar.gameObject.SetActive (true);
	}



	void Fire()
	{
		if (!GameManager.o.isGameOver) 
		{
			animator.SetInteger ("state", 2);
			Invoke ("Throw", 0.3f);
		}

		if (!GameManager.o.isGameOver)
			Invoke ("Fire", 5);
	}
	void Throw()
	{
		GameObject obj = Instantiate (bigEnemyFire);
		obj.transform.position = firePos.position;
		obj.SetActive (true);
	}
	public void Idle()
	{
		animator.SetInteger ("state", 1);
	}


	void FixedUpdate()
	{
		if (moveTowards)
		{
			transform.position = Vector3.MoveTowards (transform.position, target.position, 0.01f);
			if (transform.position == target.position) 
			{
				Invoke ("Fire", 1);
				animator.SetInteger ("state", 1);
				moveTowards = false;
			}
		}
	}





	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "bullet") 
		{
			bulletHits++;
			float fill= (float)(maxBulletHits - bulletHits) / maxBulletHits;
			healthBar.fillAmount = fill;
			if (bulletHits < maxBulletHits) 
			{
				GameManager.o.StartParticle (col.contacts [0].point);
			}
			else
			{
				animator.SetInteger ("state", 3);
				GameManager.o.GameEnd ();
			}
		}
		else if (col.gameObject.tag == "player") 
		{
			moveTowards = false;
			GameManager.o.GameOver ();
		}
	}



}
