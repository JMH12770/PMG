using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class conversationHandler : MonoBehaviour {
	public static conversationHandler control;

	private bool goOnGUI;
	private bool displayOptions;
	private string reply;
	private string displayedText;
	private string[] options;
	private Texture2D person;
	private bool checkForNext = false;

	public bool replyReady = false;
	public GUIStyle textStyle;
	public GUIStyle portrateStyle;
	public GUIStyle buttonStyle;
	public Texture2D background;

	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		}
		else if (control != this) {
			Destroy (gameObject);
		}
	}

	void Update () {
		if (checkForNext == true)  {
			if (Input.touchCount == 1) {
				replyReady = true;
				checkForNext = false;
			}
		}
	}

	public bool conversation(string[] conversation) {
		string currentText;

		replyReady = false;
		displayOptions = false;
		goOnGUI = true;
		currentText = conversation[0];
		string[] textPages = currentText.Split ('|');

		string currentPerson = conversation [1];

		Match match = Regex.Match (currentPerson, @"pags", RegexOptions.IgnoreCase);
		if (match.Success) {
			person = Resources.Load<Texture2D> ("Dev Art/pagses/" + currentPerson);
		} else {
			person = Resources.Load<Texture2D> ("Dev Art/people/" + currentPerson);
		}

		options = new string[(conversation.Length-2)];
		if (conversation.Length == 2) {
			options = null;
		} else {
			for (int i = 2; i < conversation.Length; i++){
				options[(i-2)] = conversation[i];
			}
		}
		StartCoroutine (displayText (textPages));
		return true;
	}

	public string getResponce () {
		return reply;
	}

	public void reset () {
		goOnGUI = false;
		displayOptions = false;
	}

	IEnumerator renderText() {

		textStyle.fontSize = Screen.height / 14;
		GUI.Box (new Rect (0, (Screen.height / 14)*10, Screen.width, (Screen.height / 14) * 7), background);
		GUI.DrawTexture (new Rect ((Screen.width/50)*39, (Screen.height / 14) * 9, (Screen.width / 50) * 10, (Screen.width / 50) * 10), person, ScaleMode.StretchToFill);
		GUI.Label (new Rect ((Screen.width / 50), (Screen.height / 28) * 21, (Screen.width / 50) * 38, (Screen.height / 28) * 13), displayedText, textStyle);
		if (displayOptions == true && options != null) {
			float buttonPosition = 5;
			foreach (string button in options) {
				buttonStyle.fontSize = Screen.height/20;
				if(GUI.Button(new Rect ((Screen.width/50)*buttonPosition,(Screen.height/14)*12,(Screen.width/50)*10,(Screen.height/28)*3),button,buttonStyle)) {
					reply = button;
					replyReady = true;
				}
				buttonPosition +=11;
			}
		}
		yield return null;
	}

	IEnumerator displayText(string[] textPages) {
		foreach (string currentText in textPages) {
			for (int i = 0; i <= currentText.Length; i++) {
				displayedText = currentText.Substring(0,i);
				if (!audio.isPlaying && goOnGUI == true) {
					audio.Play();
				}
				yield return new WaitForSeconds(0.05f);
			}
			audio.Stop ();
			yield return new WaitForSeconds(0.3f);
		}
		if (options != null) {
			displayOptions = true;	
			replyReady = false;
		} else {
			reply = textPages[0];
			checkForNext = true;
			displayOptions = false;
		}
	}

	void OnGUI() {
		if (goOnGUI == true) {
			StartCoroutine (renderText ());
		}
	}

}
