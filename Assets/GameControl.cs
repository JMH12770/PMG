using UnityEngine;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	public static GameControl control;

// Super Global variables
	public Vector3 cameraVector;
	public int[] aspects = new int[2];
	public Vector2 scale;
	public bool logedIn = false;

//Question of the Day Stuff
	public string QOTD;
	public string QOTDAnswer;
	public string QOTDLastUpdate;
	public bool needToUpdateQOTD = true;
	public bool answeredToday = false;

// Saved Super Global variables
	public int lifePoints = 0;
	public int points = 0;
	public int testCount = 0;
	public string isOwnedCostume = "1,0,0";
	public int currentpags = 0;
	public int playedBefore = 0;

// Saved High Scores
	public int quickMultHighScore = 0;
	public int quickDevHighScore = 0;

// This is the One Script
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
			PlayGamesPlatform.Activate();
			Load ();
		}
		else if (control != this) {
			Destroy (gameObject);
		}
	}

	void Start  (){
		statsControl.control.Load();
		StartCoroutine(googlePlay());
	}
	
//Set Pags Alias
	public string getPagsName () {
		string pagsName;
		switch (currentpags)
		{
		case 0:
			pagsName = "Pags";
			break;
		case 1:
			pagsName = "princessPags";
			break;
		case 2:
			pagsName = "spacePags";
			break;
		default:
			pagsName = "Pags";
			break;
		}

		return pagsName;
	}

//google play services stuff
	IEnumerator googlePlay () {
		Social.localUser.Authenticate((bool success) => {
			if (success){
				logedIn = true;
				if (GameControl.control.playedBefore == 0){
					Social.ReportProgress("CgkIksvUyeQTEAIQAA", 100.0f, (bool success2) => {
						if (success2){
						}
					});
				}
			}
		});
		yield return null;
	}

// Intilize lists
// Back Button Handler
	void Update () {
		string name = Application.loadedLevelName;
		if (Input.GetKeyDown (KeyCode.Escape) && name == "Dashboard" && playServicesButton.isEnabled.doOnGUI != true) {
			Application.Quit();
		} else if (Input.GetKeyDown (KeyCode.Escape) && name == "Dashboard" && playServicesButton.isEnabled.doOnGUI == true) {
			playServicesButton.isEnabled.doOnGUI = false;
			cameraScroll.cameraControl.enabled = true;
		}else if (Input.GetKeyDown (KeyCode.Escape)) {
			StartCoroutine (toDash ());
		}
	}

	IEnumerator toDash() {
		cameraVector = new Vector3 (-7, 0, -10);
		float fadeTime = GameObject.Find ("_GM").GetComponent<fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel ("Dashboard");
	}
	
	// Save/Load Handlers
	public void save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/useless.dat");

		GameData data = new GameData ();
		data.lifePoints = lifePoints;
		data.points = points;
		data.testCount = testCount;
		data.isOwnedCostume = isOwnedCostume;
		data.currentPgas = currentpags;
		data.answeredToday = answeredToday;
		data.QOTDLastUpdate = QOTDLastUpdate;

		data.quickMultHighScore = quickMultHighScore;
		data.quickDevHighScore = quickDevHighScore;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/useless.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/useless.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize(file);
			file.Close();

			lifePoints = data.lifePoints;
			points = data.points;
			testCount = data.testCount;
			currentpags = data.currentPgas;
			answeredToday = data.answeredToday;
			QOTDLastUpdate = data.QOTDLastUpdate;

			if (data.isOwnedCostume != null){
				isOwnedCostume = data.isOwnedCostume;
			}

			quickMultHighScore = data.quickMultHighScore;
			quickDevHighScore = data.quickDevHighScore;
		}
	}

// Auto save/Load

	void OnDisable() {
		save ();
		statsControl.control.save ();
	}

	void OnApplicationPause (bool paused) {
		if(paused){
			save();
			statsControl.control.save ();
		}
	}

//************************** Aspect Ratio Stuff ********************************
	void getAspectRatio() {
		Camera cam;
		cam = GameObject.Find ("Student").camera;

		if (cam.aspect >= 1.3F && cam.aspect < 1.5) {
			aspects[0] = 4;
			aspects[1] = 3;
		}
		else if (cam.aspect >= 1.5F && cam.aspect < 1.6) {
			aspects[0] = 3;
			aspects[1] = 2;
		}
		else if (cam.aspect >= 1.6F && cam.aspect < 1.66) {
			aspects[0] = 16;
			aspects[1] = 10;
		}
		else if (cam.aspect >= 1.66F && cam.aspect < 1.7) {
			aspects[0] = 5;
			aspects[1] = 3;
		}
		else {
			aspects[0] = 16;
			aspects[1] = 9;
		}
	}

	void getScale () {
		scale = new Vector2 (Screen.width/aspects[0], Screen.height/aspects[1]);
	}	
}

[Serializable]
class GameData
{
	public int lifePoints;
	public int points;
	public int testCount;
	public string isOwnedCostume;
	public int currentPgas;
	public bool answeredToday;
	public string QOTDLastUpdate;

	public int quickMultHighScore;
	public int quickDevHighScore;
}
