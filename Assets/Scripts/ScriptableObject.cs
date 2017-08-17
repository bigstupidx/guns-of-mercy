using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class ScriptableObject : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		print (Application.persistentDataPath);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.health = 100;
		data.experience = 200;
		bf.Serialize (file,data);
		file.Close ();
	}
}

[Serializable]
class PlayerData
{
	public float health;
	public float experience;
}
