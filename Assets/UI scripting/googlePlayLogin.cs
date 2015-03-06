using UnityEngine;
using GooglePlayGames;
using System.Collections;

public class googlePlayLogin : MonoBehaviour {
	public static googlePlayLogin googlePlay;


	private bool canColide = false;
	public int type;
	public string leaderBoard;
	public Sprite spr;
	public Sprite spr2;

	void Start () {
		googlePlay = this;
	}
	
	void Update()
	{
		SpriteRenderer sprRenderer= (SpriteRenderer)GetComponent<Renderer>();
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && canColide == true) {
			if (Input.GetTouch(0).deltaPosition.x > 0.3f){
				canColide = false;
			}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				canColide = true;
			} else { canColide = false;}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				sprRenderer.sprite = spr;
				switch (type)
				{
				case 1:
					Social.ShowAchievementsUI();
					break;
				case 2:
					Social.ShowLeaderboardUI();
					break;
				case 3:
					PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderBoard);
					break;
				case 4:
					Social.localUser.Authenticate((bool success) => {
						// handle success or failure
					});
					break;
				case 5:
					PlayGamesPlatform.Instance.SignOut();
					GameControl.control.logedIn = false;
					break;
				default:
					break;
				}
			}
		}else {sprRenderer.sprite = spr2;}
	}
}
