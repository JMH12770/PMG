using UnityEngine;
using System.Collections;

public class highScoreHandler : MonoBehaviour {
	public static highScoreHandler renderScore;

	void Start () {
		renderScore = this;
	}

	public void updateScores(string type) {
		switch (type)
		{
		case "quickMult":
			gameObject.GetComponent<TextMesh> ().text = "My High Score:\n" + GameControl.control.quickMultHighScore;
			Social.ReportScore(GameControl.control.quickMultHighScore, "CgkIksvUyeQTEAIQCA", (bool success) => {
				// handle success or failure
			});
			break;
		case "quickDev":
			gameObject.GetComponent<TextMesh> ().text = "My High Score:\n" + GameControl.control.quickDevHighScore;
			Social.ReportScore(GameControl.control.quickDevHighScore, "CgkIksvUyeQTEAIQDA", (bool success) => {
				// handle success or failure
			});
			break;
		default:
			break;
		}
	}
}
