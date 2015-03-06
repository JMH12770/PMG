using UnityEngine;
using System.Collections;
using GooglePlayGames;
using System;

public class questionOfTheDayHandler : MonoBehaviour {

	private bool canColide = false;
	private bool goOnGUI = false;
	private string responseString;

	public GUIStyle input;

	//Check if the person is up past midnight
	void Awake () {
		string goAgain = System.DateTime.Now.ToString();
		if (goAgain.Split ('/') [1] != GameControl.control.QOTDLastUpdate) {
			GameControl.control.QOTDLastUpdate = goAgain.Split ('/')[1];
			GameControl.control.needToUpdateQOTD = true;
			GameControl.control.answeredToday = false;
			GameControl.control.save();
		}
	}

	//If we need the question, get it
	void Start () {
	    if (GameControl.control.needToUpdateQOTD == true && GameControl.control.answeredToday != true) {
			string url = "http://cynicalrobotstudios.com/pmg/QOTD/questionOfTheDay.txt";
			gameObject.GetComponent<TextMesh> ().text = "Getting Questin of\n  the Day...";

			StartCoroutine(getQuestion(url));
			GameControl.control.needToUpdateQOTD = false;
		} else if (GameControl.control.answeredToday == true) {
			gameObject.GetComponent<TextMesh> ().text = "You already answered\nthe Question of\nthe Day.";

		} else if (GameControl.control.answeredToday != true) {
			gameObject.GetComponent<TextMesh> ().text = "Question of the Day:\n" + GameControl.control.QOTD;
		}
	}

	//Did you click it?
	void Update()
	{
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && canColide == true) {
			if (Input.GetTouch(0).deltaPosition.x > 0.3f){
				canColide = false;
			}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && GameControl.control.answeredToday != true)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				canColide = true;
			} else { canColide = false;}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true && playServicesButton.isEnabled.doOnGUI != true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				goOnGUI = true;
				cameraScroll.cameraControl.enabled = false;
			}
		}
	}

	//Input GUI
	void OnGUI () {
		if (goOnGUI == true && GameControl.control.answeredToday != true){
		GUI.Box (new Rect (Screen.width / 5, (Screen.height / 12) * 6, (Screen.width / 5) * 3, Screen.height / 12), "Answer: " + responseString);

		if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*8,Screen.width/5,Screen.height/12),"1")){
			responseString = responseString+"1";
		}
		if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*8,Screen.width/5,Screen.height/12),"2")){
			responseString = responseString+"2";
		}
		if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*8,Screen.width/5,Screen.height/12),"3")){
			responseString = responseString+"3";
		}
		if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*9,Screen.width/5,Screen.height/12),"4")){
			responseString = responseString+"4";
		}
		if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*9,Screen.width/5,Screen.height/12),"5")){
			responseString = responseString+"5";
		}
		if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*9,Screen.width/5,Screen.height/12),"6")){
			responseString = responseString+"6";
		}
		if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*10,Screen.width/5,Screen.height/12),"7")){
			responseString = responseString+"7";
		}
		if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*10,Screen.width/5,Screen.height/12),"8")){
			responseString = responseString+"8";
		}
		if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*10,Screen.width/5,Screen.height/12),"9")){
			responseString = responseString+"9";
		}
		if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*11,Screen.width/5,Screen.height/12),"Delete")){
			if (responseString.Length > 0){
				responseString = responseString.Substring(0, responseString.Length - 1);
			}
			if (responseString.Length == 0){
				goOnGUI = false;
				cameraScroll.cameraControl.enabled = true;
			}
		}
		if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*11,Screen.width/5,Screen.height/12),"0")){
			responseString = responseString+"0";
		}
		if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*11,Screen.width/5,Screen.height/12),"Submit")){
			StartCoroutine(waitForReply());
		}
		}
	}

	//Get Input
	IEnumerator waitForReply () {
		if (GameControl.control.answeredToday != true){
			string answer = responseString;

			// if correct
			if (answer == GameControl.control.QOTDAnswer){
				// Let GameControl know you answerd
				GameControl.control.answeredToday = true;

				// Do all the points stuff
				GameControl.control.points += 100;
				GameControl.control.lifePoints += 100;
				PlayGamesPlatform.Instance.IncrementAchievement(
					"CgkIksvUyeQTEAIQAQ", 100, (bool success) => {
					// handle success or failure
				});

				// Let You Know
				gameObject.GetComponent<TextMesh> ().text = "Correct! Check Back\nTomorrow for a new\nQuestion!";
				cameraScroll.cameraControl.enabled = true;
				goOnGUI = false;
			}
			else {
				gameObject.GetComponent<TextMesh> ().text = "Incorrect. Try Again\n"+GameControl.control.QOTD;
			}
			yield return null;
		}
	}

	// WWW request handler
	IEnumerator getQuestion(string url) {
		WWW www = new WWW (url);
		yield return www;
		string question = parseResponse (www.text);

		gameObject.GetComponent<TextMesh> ().text = "Question of the Day:\n" + question;
	}

	// Parse the contense of webfile
	string parseResponse(string text) {
		string[] parts = text.Split (',');
		string responce = "Hi";

		switch (parts[0])
		{
		case "quickMult":
			responce = "What is "+parts[1]+" x "+parts[2];
			GameControl.control.QOTD = responce;
			int temp1 = Int32.Parse(parts[1]);
			int temp2 = Int32.Parse(parts[2]);
			int temp = temp1 * temp2;
			GameControl.control.QOTDAnswer = temp.ToString();
			break;
		default:
			break;
		}

		return responce;
	}
}
