using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity
public class MainMenu : MonoBehaviour {

	public GameObject obj;
	public GameObject obj2;
	public GameObject obj3;
	public GameObject iapPanel;

	public GameObject loading;
	public GameObject instructionsPanel;
	public Text levelText;
	int levelCount;
	public int levelsUnlocked;
	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1f;
		levelCount = 0;
		levelText.text = (levelCount + 1) + "";
		PlayerPrefs.SetInt ("level", 0);
		levelsUnlocked = PlayerPrefs.GetInt ("levelsUnlocked");
	}
	
	public void Button_Click(Button btn)
	{
		if (btn.name == "play") 
		{
			
			btn.gameObject.SetActive (false);
			loading.SetActive (true);
//			obj.SetActive (true);
//			obj2.SetActive (false);
//			obj3.SetActive (false);

			GameManager.LoadGame ();
		}
//		else if (btn.name == "up") 
//		{
//			if (levelCount < levelsUnlocked) 
//			{
//				levelCount++;
//				levelText.text = (levelCount + 1) + "";
//			}	
//		}
//		else if (btn.name == "down") 
//		{
//			if (levelCount > 0)
//			{
//				levelCount--;
//				levelText.text = (levelCount + 1) + "";
//			}
//		}
//		else if (btn.name == "go") 
//		{
//			PlayerPrefs.SetInt ("level", levelCount);
//			GameManager.LoadGame ();
//		}
		else if (btn.name == "removeAds") 
		{
			iapPanel.SetActive (true);
		}
		else if (btn.name == "howTo") 
		{
			instructionsPanel.SetActive (true);
		}
		else if (btn.name == "backIns") 
		{
			instructionsPanel.SetActive (false);
		}
		else if (btn.name == "back") 
		{
			iapPanel.SetActive (false);
		}
	}




	public void OnPurchaseComplete()
	{
		PlayerPrefs.SetInt ("isPurchased", 1);
		iapPanel.SetActive (false);
	}

	public void OnPurchaseFail()
	{
		
	}
}
