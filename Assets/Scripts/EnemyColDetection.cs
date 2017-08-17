using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColDetection : MonoBehaviour {

	Animator animator;
	PolygonCollider2D pc;
	SpriteRenderer sr;
	public Sprite []sprites;

	public float wait = 0.05f;

	void Awake () 
	{
		animator = GetComponentInParent<Animator> ();
		pc = GetComponent<PolygonCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
	
		
	}
	void OnEnable()
	{
		animator.Rebind ();
//		animator.SetInteger ("state", 0);
		pc.enabled = true;
		StartCoroutine (Anim ());

//		Color c= sr.color;
//		c.a = 255;
//		sr.color = c;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "bullet") 
		{
			animator.SetInteger ("state", 1);
			pc.enabled = false;
//			pc.enabled = false;
//			gameObject.SetActive(false);
			GameManager.o.StartParticle (transform.position);

		}
	}

	IEnumerator Anim()
	{
//		print (Anim);
		for (int i = 0; i < sprites.Length; i++) 
		{
			sr.sprite = sprites [i];
			yield return new WaitForSeconds (wait);

		}

		StartCoroutine (Anim ());
	}
}
