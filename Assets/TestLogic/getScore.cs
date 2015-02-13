using UnityEngine;
using GooglePlayGames;
using System.Collections;

public class getScore : MonoBehaviour {
	public static getScore over;

	public bool isOver = false;
	public int pointModifyer = 10;
	public string type;

	private TextMesh resultSteam;

	void Awake () {
		over = this;
		resultSteam = GameObject.Find ("resultStream").GetComponent<TextMesh> ();
	}

	void Update () {
		if (isOver == true) {
			if (GameObject.Find ("Student").transform.position.x < 48f) {
				GameObject.Find ("Student").transform.Translate (0.5f, 0f, 0f);
			}
		}
	}

	public void calcScore() {
		int totalAnswered = 0;
		int totalCorrect = 0;

		foreach ( int key in generateTest.generate.testData.Keys)
		{ 
			totalAnswered++;
			string s  = (generateTest.generate.testData[key] as string[])[3];
			if (s == "true"){
				totalCorrect++;
			}
		}

		int points = totalCorrect * pointModifyer;
		float pointBuff = statsControl.control.calculateMultiplyer ();
		points = (int)(pointBuff * (float)points);

		//Google Play Points stuff
		Social.ReportScore(points, "CgkIksvUyeQTEAIQCA", (bool success) => {
			// handle success or failure
		});
		PlayGamesPlatform.Instance.IncrementAchievement(
			"CgkIksvUyeQTEAIQAQ", points, (bool success) => {
			// handle success or failure
		});

		// My Points Stuff
		switch (type)
		{
		case "quickMult":
			if (points >= GameControl.control.quickMultHighScore){
				GameControl.control.quickMultHighScore = points;
			}
			break;
		case "quickDev":
			if (points >= GameControl.control.quickDevHighScore){
				GameControl.control.quickDevHighScore = points;
			}
			break;
		default:
			break;
		}

		GameControl.control.points += points;
		GameControl.control.lifePoints += points;
		GameControl.control.testCount++;
		GameControl.control.save ();

		resultSteam.text = "You scored " + totalCorrect + " out of " + totalAnswered + "\n for a total of " + points + " points!\n Total Points: "+ GameControl.control.points+"";
	}
}
