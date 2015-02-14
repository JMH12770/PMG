using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class storeControl : MonoBehaviour {
	public static storeControl control;

	//Static store Objects
	public string levelOwned = "0,0,0,0,0,0,0,0,0,0";
	public string[] names = new string[] {"clock","pagsDesk","studentDesk","board","calc","comp","pens","cat","horse","rock"};
	public Hashtable descriptions = new Hashtable();
	public Hashtable prices = new Hashtable();
	public Hashtable multiplyer = new Hashtable();

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
			Load ();
		}
		else if (control != this) {
			Destroy (gameObject);
		}
	}

	void Start () {
		//Jacob This is where you set the starting price for each Item
		prices["clock"] = 10;
		prices["pagsDesk"] = 10;
		prices["studentDesk"] = 10;
		prices["board"] = 10;
		prices["calc"] = 10;
		prices["comp"] = 10;
		prices["pens"] = 10;
		prices["cat"] = 10;
		prices["horse"] = 10;
		prices["rock"] = 10;

		//This is where you set what percentage the item will get more expensive by
		multiplyer["clock"] = 1.5;
		multiplyer["pagsDesk"] = 1.5;
		multiplyer["studentDesk"] = 1.5;
		multiplyer["board"] = 1.5;
		multiplyer["calc"] = 1.5;
		multiplyer["comp"] = 1.5;
		multiplyer["pens"] = 1.5;
		multiplyer["cat"] = 1.5;
		multiplyer["horse"] = 1.5;
		multiplyer["rock"] = 1.5;

		//Ignore this part
		descriptions["clock"] = "This is a Basic Clock|Now the clock will\nwork|more Description|The Best Clock";
		descriptions["pagsDesk"] = "This is Pags' Desk|Place Holder|Place Holder|Place Holder";
		descriptions["studentDesk"] = "This is your Desk|Place Holder|Place Holder|Place Holder";
		descriptions["board"] = "This is stuff|Place Holder|Place Holder|Place Holder";
		descriptions["calc"] = "This is a Calculator|Place Holder|Place Holder|Place Holder";
		descriptions["comp"] = "This is Pags' Computer|Place Holder|Place Holder|Place Holder";
		descriptions["pens"] = "This is your pens|Place Holder|Place Holder|Place Holder";
		descriptions["cat"] = "So Many Cats|Place Holder|Place Holder|Place Holder";
		descriptions["horse"] = "Does nothing|Place Holder|Place Holder|Place Holder";
		descriptions["rock"] = "This is the class\npet|Place Holder|Place Holder|Place Holder";
	}

	// Save Function
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/pointless.dat");
		
		StoreData data = new StoreData ();

		data.levelOwned = levelOwned;


		bf.Serialize (file, data);
		file.Close();
	}

	// Load Function
	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/pointless.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/pointless.dat", FileMode.Open);
			StoreData data = (StoreData)bf.Deserialize(file);
			file.Close();

			levelOwned = data.levelOwned;
		}
	}

	// Auto save/Load
	
	void OnDisable() {
		Save ();
	}
	
	void OnApplicationPause () {
		Save();
	}
}

[Serializable]
class StoreData
{
	public string levelOwned;
}
