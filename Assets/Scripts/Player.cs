using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{


	public Slider slider;
	AudioSource audioSource;
	SpriteRenderer fireSR;
	public List<Sprite> fireSprites = new List<Sprite> ();
	public Sprite playerDeadSprite;

	public AudioClip fireClip;
	public GameObject bulletPrefab;
	List<GameObject> bullets = new List<GameObject> ();

	public GameObject[] powers= new GameObject[3];
	Vector2 matOffset;
	public Material mat;

	Rigidbody2D rb;
	Animator animator;

	Transform firePos;
	Transform hand;
	Transform head;
	float force;
	float maxVelocity;
	int bulletsCounter;
	bool isPointerDown;
	int noOfBullets;
	bool isCollided;
	bool isMouseButtonUp;
	bool isAlive;
	int bulletHits;
	int maxBulletsHits;
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		fireSR = transform.GetChild (0).GetChild (0).GetComponent<SpriteRenderer> ();
		hand= transform.GetChild (0);
		head= transform.GetChild (1);
		firePos = hand.GetChild (0);
		noOfBullets = 20;
		bulletHits = 0;
		maxBulletsHits = 3;
		isAlive = true;
//		slider.value = 0f;
		GameObject obj;
		for (int i = 0; i < noOfBullets; i++) 
		{
			obj = Instantiate (bulletPrefab);
			bullets.Add (obj);
		}
		bulletsCounter = 0;
		force = 10;
		maxVelocity = 4;
		isPointerDown = false;
		Invoke ("Fire", 1);
	}



	void Fire()
	{
		while (bullets [bulletsCounter].activeInHierarchy)
		{
			bulletsCounter = (bulletsCounter + 1) % noOfBullets;
		}
		StartCoroutine (FireAnim ());
		audioSource.PlayOneShot (fireClip);

		bullets [bulletsCounter].transform.position = firePos.position;
		bullets [bulletsCounter].transform.eulerAngles = hand.eulerAngles;
//		bullets [bulletsCounter].transform.eulerAngles = new Vector3 (0, 0, -slider.value);
//		bullets [bulletsCounter].transform.localEulerAngles = new Vector3 (0, 0, -(slider.value/ 3));

		bullets [bulletsCounter].SetActive (true);

		if (!GameManager.o.isGameOver)
			Invoke ("Fire", 0.3f);
	}



	IEnumerator FireAnim()
	{
		for (int i = 0; i < fireSprites.Count; i++)
		{
			fireSR.sprite = fireSprites [i];
			yield return new WaitForSeconds (0.05f);
		}
		fireSR.sprite = null;
	}



	Vector2 touchStartPos;
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			Jump ();
		}
		if (Input.touchCount > 0) 
		{
			Touch t = Input.GetTouch (0);
			if (t.phase == TouchPhase.Began) 
			{
				touchStartPos = t.position;	
			}
			else if (t.phase == TouchPhase.Moved)
			{
				if (t.position.y > touchStartPos.y + 100) 
				{
					if (isMouseButtonUp && isCollided) 
					{
						isMouseButtonUp = isCollided = false;
						Jump ();
//						animator.SetInteger ("state", 2);
//						rb.AddForce (Vector3.up * 300);
					}
					//swipe up - jump
				}
				else if (t.position.y < touchStartPos.y - 100) 
				{
					//swipe down - shrink
				}
			}
			else if (t.phase == TouchPhase.Ended)
			{
				isMouseButtonUp = true;
				if (isCollided)
				{
					if(isPointerDown)
						animator.SetInteger ("state", 1);
					else 
						animator.SetInteger ("state", 0);
				}

			}
		}
	}

	public void Jump()
	{
		animator.SetInteger ("state", 2);
		rb.AddForce (Vector3.up * 300);
	}


	public void PointerDown(Button btn)
	{
		animator.SetInteger ("state", 1);
		if (btn.name == "left")
			force = -10;
		else if (btn.name == "right")
			force = 10;
		isPointerDown = true;
	}



	public void PointerUp()
	{
		animator.SetInteger ("state", 0);
		isPointerDown = false;
		rb.velocity = Vector2.zero;
	}



	void FixedUpdate () 
	{
		if (isPointerDown) 
		{
			if (rb.velocity.magnitude < maxVelocity) 
			{
				rb.AddForce (Vector2.right * force);
			}
		}

		matOffset.x += 0.001f;
		mat.SetTextureOffset ("_MainTex", matOffset);
	}



	void OnCollisionEnter2D(Collision2D col)
	{
		isCollided = true;
		if (isMouseButtonUp)
		{
			if(isPointerDown)
				animator.SetInteger ("state", 1);
			else 
				animator.SetInteger ("state", 0);
		}
		if (col.gameObject.tag == "enemy") 
		{
//			GameManager.o.GameOver ();
			Hit ();
		}
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "enemy") 
		{
//			GameManager.o.GameOver ();
			Hit ();
		}
	}

	void Hit()
	{
		if (isAlive) 
		{
			powers [bulletHits].GetComponent<Image> ().sprite = playerDeadSprite;
			bulletHits++;
			if (bulletHits >= maxBulletsHits) 
			{
				isAlive = false;
				GameManager.o.GameOver ();
			}
		}

	}



	public void OnSliderValueChange()
	{
		hand.localEulerAngles = new Vector3 (0, 0, -(Mathf.Abs (slider.value) / 3) - 60);
		head.localEulerAngles = new Vector3 (0, 0, (-Mathf.Abs (slider.value) / 4) + 25);

		Flip ();
	}



	bool isFacingRight = true;
	void Flip()
	{
		if (slider.value > 0 && !isFacingRight) 
		{
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			isFacingRight = !isFacingRight;
		}
		else if (slider.value < 0 && isFacingRight)
		{
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			isFacingRight = !isFacingRight;

		}
	}
}
