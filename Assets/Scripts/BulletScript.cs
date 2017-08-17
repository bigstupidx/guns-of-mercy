using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	Rigidbody2D rb;
	// Use this for initialization
	void Awake () 
	{
		rb = GetComponent<Rigidbody2D> ();
		
		
	}
	void OnEnable()
	{
//		print (new Vector3 (Mathf.Clamp (transform.rotation.eulerAngles.x, -15f, 15f), transform.rotation.eulerAngles.y, Mathf.Clamp (transform.rotation.eulerAngles.z, -15f, 15f)));

		rb.AddForce (transform.up * 1200);
		Invoke ("Disable", 2);
	}

	void Disable()
	{
		gameObject.SetActive (false);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "enemy") 
		{
			CancelInvoke ("Disable");
			Disable ();
		}
	}
	

}
