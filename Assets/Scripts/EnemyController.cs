using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	float speed;
	float speedVal;
	void Awake () 
	{
		speedVal = 0.05f;
//		animator = GetComponent<Animator> ();
	}

	void OnEnable()
	{
		if (transform.position.x < 0) 
		{
			speed = speedVal;
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
		else
			speed = -speedVal;
	}


	void Fire()
	{
		
	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		transform.Translate (speed, 0, 0);
		if (transform.position.x > 10) 
		{
			speed = 0;
			transform.position = new Vector3 (9.99f, transform.position.y, transform.position.z);
			StartCoroutine (Flip(1));
		}
		else if (transform.position.x < -10) 
		{
			speed = 0;
			transform.position = new Vector3 (-9.99f, transform.position.y, transform.position.z);
			StartCoroutine (Flip(2));
		}

	}

	IEnumerator Flip(int i)
	{
		yield return new WaitForSeconds (1);
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		if (i == 1) 
		{
			speed = -speedVal;
		}
		else if (i == 2) 
		{
			speed = speedVal;
		}
	}

	bool isDead = false;
	public void DisableMe()
	{
		if (!isDead)
		{
			isDead = true;
			GameManager.o.EnemyDie (gameObject);
			Destroy (gameObject);
			//		gameObject.SetActive (false);
		}

	}
}
