using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	Animator animator;
	public Transform target;
	public Collider2D meraCollider;

	Transform firePos;
	bool canMoveTowards;
	float speed = 0.02f;
	int bulletHits;
	int maxBulletsHit;
	float startX;
	float randAttackDistance;
	bool isAlive;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
		target = GameManager.o.player;
		meraCollider = GetComponent<Collider2D> ();
		firePos = transform.GetChild (0);
		startX = transform.position.x;
		isAlive = true;
		canMoveTowards = true;
		bulletHits = 0;
		maxBulletsHit = 3;
		randAttackDistance = Random.Range (4, 5);
		speed = Random.Range (0.02f, 0.03f);
	}




	void OnEnable()
	{
		if (transform.position.x < 0) 
		{
			transform.localScale= new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else 
		{
			transform.localScale= new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
	}


	// Update is called once per frame
	void FixedUpdate ()
	{
		if (canMoveTowards)
		{
			if (Mathf.Abs (transform.position.x - startX) > randAttackDistance)
			{
				Attack ();
			}
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed);
		}
	}

	void Attack()
	{
		startX = transform.position.x;
		canMoveTowards = false;
		animator.SetInteger ("state", 1);
		Invoke ("Throw", 0.5f);
	}

	void Throw()
	{
		if (isAlive) 
		{
			GameObject obj=	GameManager.o.GetZombieFire ();
			obj.transform.position = firePos.position;
			obj.SetActive (true);
		}

	}
	public void Walk()
	{
		if (isAlive)
		{
			randAttackDistance = Random.Range (3, 4);
			canMoveTowards = true;
			animator.SetInteger ("state", 0);
		}
	}



	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "bullet") 
		{
			bulletHits++;
			GameManager.o.StartParticle (transform.position);
			if (bulletHits >= maxBulletsHit) 
			{
				isAlive = false;
				canMoveTowards = false;
				animator.SetInteger ("state", 2);
				meraCollider.enabled = false;
			}

		}
		if (col.gameObject.tag == "player") 
		{
			canMoveTowards = false;
		}
	}

	public void DestroyMe()
	{
		
		GameManager.o.EnemyDie (gameObject);
		Destroy (gameObject);
	}
}
