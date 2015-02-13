using UnityEngine;
using GooglePlayGames;
using System.Collections;

public class playServicesButton : MonoBehaviour {
	public static playServicesButton isEnabled;

	public Sprite spr;
	public Sprite spr2;
	public float difference = 1;
	public Texture2D background;
	public Texture2D redX;
	public GUIStyle buttonStyle;
	public bool doOnGUI = false;

	private bool canColide = false;

	void Awake () {
		isEnabled = this;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		if (GameControl.control.logedIn == true) {
			sprRenderer.sprite = spr2;
		} else {sprRenderer.sprite = spr;}
	}

	void Update()
	{
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && canColide == true) {
			if (Input.GetTouch(0).deltaPosition.x > 0.3f){
				canColide = false;
			}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				canColide = true;
			} else { canColide = false;}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true && doOnGUI != true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				if (GameControl.control.logedIn == false){
					Social.localUser.Authenticate((bool success) => {
						if (success){
							GameControl.control.logedIn = true;
							sprRenderer.sprite = spr2;
						} else {sprRenderer.sprite = spr;}
					});
				} else {
					cameraScroll.cameraControl.enabled = false;
					doOnGUI = true;
				}
			}
		}
	}

	void FixedUpdate () {
		difference = (Screen.width / 12.8f) * 100;
	}

	void OnGUI () {
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		buttonStyle.fontSize = Screen.width / 30;
		buttonStyle.padding.left = Screen.width / 40;

		if (doOnGUI == true){
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),background, ScaleMode.StretchToFill);
			if(GUI.Button (new Rect((Screen.width/5)*4,Screen.height/7,0.01f*difference,0.01f*difference), redX)){
				doOnGUI = false;
				cameraScroll.cameraControl.enabled = true;
			}
			if(GUI.Button (new Rect(Screen.width/3,Screen.height/7,Screen.width/3,Screen.height/7), "Leader Boards", buttonStyle)){
				Social.ShowLeaderboardUI();
			}
			if(GUI.Button (new Rect(Screen.width/3,(Screen.height/7)*3,Screen.width/3,Screen.height/7), "Achievements", buttonStyle)){
				Social.ShowAchievementsUI();
			}
			if(GUI.Button (new Rect(Screen.width/3,(Screen.height/7)*5,Screen.width/3,Screen.height/7), "Log Out", buttonStyle)){
				PlayGamesPlatform.Instance.SignOut();
				GameControl.control.logedIn = false;
				doOnGUI = false;
				cameraScroll.cameraControl.enabled = true;
				sprRenderer.sprite = spr;
			}
		}
	}
}
