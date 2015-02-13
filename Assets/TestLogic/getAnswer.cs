using UnityEngine;
using System.Collections;

public class getAnswer : MonoBehaviour {
	public static getAnswer checkAnswer;

	public Sprite spr;
	public Sprite spr2;
	
	private bool goOnGUI = false;
	private bool canColide = false;
	private string input = "";

	void Awake () {
		checkAnswer = this;
	}

	void Update() {
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		if (goOnGUI == true && generateTest.generate.timeUp == true) {
			goOnGUI = false;
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && generateTest.generate.timeUp == false)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				canColide = true;
			} else { canColide = false;}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				sprRenderer.sprite = spr;
				goOnGUI = true;
			}
		}
	}

	void OnGUI () {
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		if (goOnGUI == true){
			GUI.Box (new Rect (Screen.width / 5, (Screen.height / 12) * 6, (Screen.width / 5) * 3, Screen.height / 12), "Answer: " + input);
			
			if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*8,Screen.width/5,Screen.height/12),"1")){
				input = input+"1";
			}
			if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*8,Screen.width/5,Screen.height/12),"2")){
				input = input+"2";
			}
			if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*8,Screen.width/5,Screen.height/12),"3")){
				input = input+"3";
			}
			if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*9,Screen.width/5,Screen.height/12),"4")){
				input = input+"4";
			}
			if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*9,Screen.width/5,Screen.height/12),"5")){
				input = input+"5";
			}
			if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*9,Screen.width/5,Screen.height/12),"6")){
				input = input+"6";
			}
			if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*10,Screen.width/5,Screen.height/12),"7")){
				input = input+"7";
			}
			if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*10,Screen.width/5,Screen.height/12),"8")){
				input = input+"8";
			}
			if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*10,Screen.width/5,Screen.height/12),"9")){
				input = input+"9";
			}
			if (GUI.Button(new Rect(Screen.width/5, (Screen.height/12)*11,Screen.width/5,Screen.height/12),"Delete")){
				if (input.Length > 0){
					input = input.Substring(0, input.Length - 1);
				}
				if (input.Length == 0){
					goOnGUI = false;
				}
			}
			if (GUI.Button(new Rect((Screen.width/5)*2, (Screen.height/12)*11,Screen.width/5,Screen.height/12),"0")){
				input = input+"0";
			}
			if (GUI.Button(new Rect((Screen.width/5)*3, (Screen.height/12)*11,Screen.width/5,Screen.height/12),"Submit")){
				sprRenderer.sprite = spr2;
				generateTest.generate.wasAnswered = true;
				generateTest.generate.questionData[2] = input;
				goOnGUI = false;
				input = "";
			} else {generateTest.generate.wasAnswered = false;}
		}
	}
}
