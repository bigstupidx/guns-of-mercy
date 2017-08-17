using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization

	public Transform leftBoundary;
	public Transform rightBoundary;

	public GameObject gameOverPanel;
	public GameObject levelCompletePanel;
	public GameObject pausePanel;

	public GameObject []enemiesPrefab;
	public GameObject bigEnemy;

	public GameObject zombieFire;
	public List<GameObject> zombieFiresList = new List<GameObject> ();
//	public GameObject flyingEnemyPrefab1;
//	public GameObject flyingEnemyPrefab2;
	List<GameObject> enemyList;

	public GameObject particelPrefab;

	public List<ParticleSystem> particleList = new List<ParticleSystem> ();
	public List<GameObject > list = new List<GameObject> ();
	public Transform player;
	public Text scoreText;

	public int enemiesCount;
	public bool isGameOver;
	public int score;
	public int batches;
	public int particlesCount;
	public int enemiesKilled;
	public int[] enemiesList;
	public int level;
	public static GameManager o;

	void Awake ()
	{

		Time.timeScale = 1f;
//		PlayerPrefs.SetInt ("level", 2);
		if (o == null)
			o = this;
		else
			Destroy (gameObject);

		enemiesKilled = PlayerPrefs.GetInt ("enemiesKilled");
		level = PlayerPrefs.GetInt ("level");
		enemiesList = new int[8];

		enemiesList [0] = 0;
		enemiesList [1] = 10;
		enemiesList [2] = 25;
		enemiesList [3] = 40;
		enemiesList [4] = 60;
		enemiesList [5] = 80;
		enemiesList [6] = 100;

		score = enemiesList [level];
//		level = 5;
//		score = 99;
		scoreText.text = score + "/" + enemiesList [level+1];
		float aspect = Camera.main.aspect;

//		Camera.main.aspect = 1.5f;
		GameObject obj1;
		for (int i = 0; i < 5; i++) 
		{
			obj1 = Instantiate (particelPrefab);
			obj1.transform.position = new Vector3 (100, 0, 0);
			particleList.Add (obj1.GetComponent<ParticleSystem> ());
		}

		for (int i = 0; i < 10; i++)
		{
			obj1 = Instantiate (zombieFire);
			zombieFiresList.Add (obj1);			
		}

//		score = PlayerPrefs.GetInt ("score");

		isGameOver = false;
		enemiesCount = 0;
//		enemyList = new List<GameObject> ();
//		GameObject obj;
//		for (int i = 0; i < 20; i++) 
//		{
//			obj = Instantiate (enemyPrefab);
//			enemyList.Add (obj);			
//		}
		GenerateEnemy ();
		AllignBoundaries ();
	}


	void AllignBoundaries()
	{
//		Vector3 pos_temp = Camera.main.ScreenToWorldPoint(new Vector3( (((Screen.width/posx) ) ), (Screen.height / posy) , this.transform.position.z ) );
		Vector3 pos = Camera.main.ViewportToWorldPoint (new Vector3 (1.12f, 0, 0));
		rightBoundary.position = new Vector3 (pos.x, rightBoundary.position.y, 0);
//		pos = Camera.main.ViewportToWorldPoint (new Vector3 (-1.12f, 0, 0));
		leftBoundary.position = new Vector3 (-pos.x, leftBoundary.position.y, 0);

//		leftBoundary.position = new Vector3 (-Screen.width / 82f, leftBoundary.position.y, leftBoundary.position.z);
//		rightBoundary.position = new Vector3 (Screen.width / 82f, leftBoundary.position.y, leftBoundary.position.z);
	}
	
	public  static void LoadGame()
	{
		SceneManager.LoadSceneAsync ("game");
	}

	public void GameOver()
	{
		if (!isGameOver)
		{
			isGameOver = true;
			gameOverPanel.SetActive (true);
			ShowAd ();
		}

	}

	public void LevelCompleted()
	{
//		int levelsUnlocked = PlayerPrefs.GetInt ("levelsUnlocked");
//		if (levelsUnlocked <= level && levelsUnlocked < 5) 
//		{
//			levelsUnlocked++;
//			PlayerPrefs.SetInt ("levelsUnlocked", levelsUnlocked);
//		}
		if (level < 5) 
		{
			isGameOver = true;
			levelCompletePanel.SetActive (true);
			ShowAd ();
		}
		else
		{
			int j = list.Count;
			for (int i = 0; i < j; i++) 
			{
				Destroy (list [0]);
				list.RemoveAt (0);
			}
			CancelInvoke ();
			bigEnemy.SetActive (true);
		}

	}

	public void UI_Manager(Button btn)
	{
		if (btn.name == "pause") 
		{
			Time.timeScale = 0;
			pausePanel.SetActive (true);
		}
		else if (btn.name == "resume") 
		{
			pausePanel.SetActive (false);
			Time.timeScale = 1;
		}
		else if (btn.name == "home") 
		{
			SceneManager.LoadScene ("home");
		}
		else if (btn.name == "next") 
		{
			level++;
			level = level % 6;
			PlayerPrefs.SetInt ("level", level);
			SceneManager.LoadScene ("game");
		}
		else if (btn.name == "replay") 
		{
			SceneManager.LoadScene ("game");
		}
		else if (btn.name == "resume") {
		}
		else if (btn.name == "resume") {
		}
	}


	int rand;
	GameObject obj;




	public void GenerateEnemy()
	{
		if (enemiesCount < 5) 
		{
			rand = Random.Range (0, 2);
			obj = Instantiate (enemiesPrefab [rand]);

			rand = Random.Range (0, 2);
			if (rand == 0)
				obj.transform.position = new Vector3 (-10.5f, obj.transform.position.y, 0);
			else if (rand == 1)
				obj.transform.position = new Vector3 (10.5f, obj.transform.position.y, 0);
			
			obj.SetActive (true);
			list.Add (obj);
			enemiesCount++;
		}
		if (!isGameOver)
			Invoke ("GenerateEnemy", 2);

	}
//	public void GenerateEnemy()
//	{
//		if (enemiesCount < 8)
//		{
//			rand = Random.Range (0, 2);
//			obj = Instantiate (enemiesPrefab [rand]);
////			if (rand == 0)
////				obj = Instantiate (flyingEnemyPrefab1);
////			else if (rand == 1)
////				obj = Instantiate (flyingEnemyPrefab2);
//
//			rand = Random.Range (0, 2);
//			if (rand == 0)
//				obj.transform.position = new Vector3 (-10, Random.Range (2.5f, 4), 0);
//			else if (rand == 1)
//				obj.transform.position = new Vector3 (10, Random.Range (2.5f, 4), 0);
//
//			obj.SetActive (true);
//			list.Add (obj);
//			enemiesCount++;
//		}
////		for (int i = 0; i < enemyList.Count; i++) 
////		{
////			if (!enemyList [i].activeInHierarchy) 
////			{
////				int rand2 = Random.Range (0, 2);
////				if (rand2 == 0)
////					enemyList [i].transform.position = new Vector3 (-10, Random.Range (2.5f, 4), 0);
////				else if (rand2 == 1)
////					enemyList [i].transform.position = new Vector3 (10, Random.Range (2.5f, 4), 0);
////
////				enemyList [i].SetActive (true);
////				break;
////			}
////		}
//		if (!GameManager.o.isGameOver)
//		{
//			rand = Random.Range (0, 2);
//			if (rand == 0)
//			{
//				Invoke ("GenerateEnemy", 2);
//			}
//			else if (rand == 1) 
//			{
//				Invoke ("GenerateScrollingEnemy", 2);
//			}
//
//		}
//
//	}
//	public void GenerateScrollingEnemy()
//	{
//
//		if (enemiesCount < 8)
//		{
//			obj = Instantiate (enemiesPrefab [2]);
//
//			rand = Random.Range (0, 2);
//			if (rand == 0)
//				obj.transform.position = new Vector3 (-10.5f, obj.transform.position.y, 0);
//			else if (rand == 1)
//				obj.transform.position = new Vector3 (10.5f, obj.transform.position.y, 0);
//
//			obj.SetActive (true);
//			list.Add (obj);
//			enemiesCount++;
//		}
//
//		if (!GameManager.o.isGameOver)
//		{
//			rand = Random.Range (0, 2);
//			if (rand == 0)
//			{
//				Invoke ("GenerateEnemy", 2);
//			}
//			else if (rand == 1) 
//			{
//				Invoke ("GenerateScrollingEnemy", 2);
//			}
//
//		}
//	}

	int zombieFireCounter;
	public GameObject GetZombieFire()
	{
		zombieFireCounter = (zombieFireCounter + 1) % 10;

		return zombieFiresList [zombieFireCounter];
	}



	public void StartParticle(Vector3 pos)
	{
		particleList [particlesCount].transform.position = pos;
		particleList [particlesCount].Play ();
		particlesCount = (particlesCount + 1) % 5;
	}

	public void EnemyDie(GameObject obj)
	{
		list.Remove (obj);
		enemiesCount--;
		scoreText.text = ++score + "/" + enemiesList [level+1];
		if (score >= enemiesList [level + 1]) 
		{
			LevelCompleted ();
		}
	}

	public void GameEnd()
	{
//		PlayerPrefs.SetInt ("level", 0);
		bigEnemy.SetActive (false);
		isGameOver = true;
		levelCompletePanel.SetActive (true);
		ShowAd ();
	}




	public void ShowAd()
	{
		int isPurchased = PlayerPrefs.GetInt ("isPurchased");
		if (isPurchased < 1)
		{
			int ad = PlayerPrefs.GetInt ("ad");
			if (ad % 2 == 0) 
			{
				AdMob.o.ShowVideo ();
			}
			ad++;
			PlayerPrefs.SetInt ("ad", ad);
		}
	}
	public void RateUs()
	{
		Application.OpenURL("market://details?id=packageName/");
	}
}
