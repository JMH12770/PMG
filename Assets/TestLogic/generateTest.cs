using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class generateTest : MonoBehaviour {
	public static generateTest generate;
	
	public float timeLimit = 60f;
	public Hashtable testData = new Hashtable ();
	public string[] questionData = new string[4];
	public int i = 0;

	public bool wasAnswered = false;
	public bool timeUp = false;

	private bool testStarted = false;

	private TextMesh testText;
	private TextMesh testTitleText;

	private string testType;
	private string timeText;

	void Awake () {
		generate = this;
		testText = GameObject.Find ("testText").GetComponent<TextMesh>();
		testTitleText = GameObject.Find ("testTitleText").GetComponent<TextMesh>();
	}

	void Update () {
		if (testStarted == true) {
			getTime ();
			if (GameObject.Find ("Student").transform.position.x < 24) {
				GameObject.Find ("Student").transform.Translate (0.5f, 0f, 0f);
			}

			if (questionData[2] != null) {
				if (questionData[1] == questionData[2]){
					questionData[3] = "true";
				} else {
					questionData[3] = "false";
				}
			
				testData.Add (i,questionData);
				Invoke (testType,0);
			}
		}
	}

	public void newTest (string type) {
		testStarted = true;
		i = 0;
		testText.text = null;
		switch (type)
		{
		case "quickMult":
			testType = "quickMult";
			timeText = "Quick multiplication";
			getScore.over.type = "quickMult";
			break;
		case "quickDev":
			testType = "quickDev";
			timeText = "Quick devishion";
			getScore.over.type = "quickDev";
			break;
		default:
			break;
		}
		Invoke (testType,0);
	}

	void getTime() {
		timeLimit -= Time.deltaTime;
		testTitleText.text = timeText+"\nTime Remaining: " + timeLimit.ToString("F1");
		
		if (timeLimit <= 0) {
			timeLimit = 0;
			timeUp = true;
			testStarted = false;
			getScore.over.calcScore();
			getScore.over.isOver = true;
		}
	}

	//******************************** TEST TYPES ******************************************* 

	void quickMult() {
		questionData = new string[4];
		i++;

		int rnd1 = Random.Range(1,12);
		int rnd2 = Random.Range(1,12);
		
		int answer = rnd1 * rnd2;

		testText.text ="What is: "+rnd1+" X "+rnd2;
			
		questionData[0] = (""+rnd1+" X "+rnd2+"");
		questionData[1] = (answer.ToString());
	}

	void quickDev() {
		questionData = new string[4];
		i++;
		bool works = false;
		int answer;
		string last;

		if (testData[i-1] != null && i > 0) {
			last = (testData[(i-1)] as string[])[1];
		} else {last = "0";}

		while (works == false){
			int rnd1 = Random.Range(1,100);
			int rnd2 = Random.Range(1,100);

			if(rnd1 % rnd2 == 0 && (rnd1/rnd2) != 1 && rnd1 != 1 && rnd2 != 1 &&  last != (rnd1/rnd2).ToString())
			{
				works = true;
				answer = rnd1/rnd2;
				testText.text ="What is: "+rnd1+" / "+rnd2;
				
				questionData[0] = (""+rnd1+" / "+rnd2+"");
				questionData[1] = (answer.ToString());
			}
		}
	}

	bool IsThisInteger(float myFloat)
	{
		return Mathf.Approximately(myFloat, Mathf.RoundToInt(myFloat));
	}
}
