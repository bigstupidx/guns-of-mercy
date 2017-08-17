using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour {

	public Transform target;
	Transform child;


	float dist;

//	void Start()
//	{

//	}
	void Start () 
	{
		transform.LookAt (target.position);
//		child = transform.GetChild (0);
		dist = Vector3.Distance (transform.position, target.position);
//		child.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, dist);
//		child.localPosition = new Vector3 (0, 0, dist / 2);
		//StartCoroutine (Anim ());

	}
	
	IEnumerator Anim()
	{
//		yield return new WaitForSeconds (0.5f);
		for (int i = 0; i < dist; i++)
		{
			child.localScale = new Vector3 (child.localScale.x, child.localScale.y, child.localScale.z + 1);
			yield return new WaitForSeconds (0.01f);
			child.localPosition = new Vector3 (0, 0, child.localPosition.z + 0.5f);	
			yield return new WaitForSeconds (0.01f);
		}
//		yield return new WaitForSeconds (0.5f);

		for (int i = 0; i < dist; i++)
		{
			child.localScale = new Vector3 (child.localScale.x, child.localScale.y, child.localScale.z - 1);
			yield return new WaitForSeconds (0.01f);
			child.localPosition = new Vector3 (0, 0, child.localPosition.z - 0.5f);
			yield return new WaitForSeconds (0.01f);
		}

		StartCoroutine (Anim ());
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "enemy") 
		{
			print ("enemy");
		}
		else if (col.gameObject.tag == "Fire") 
		{
			print ("Fire");
		}
	}
}
