using UnityEngine;
using System.Collections;

public class newTestButton : MonoBehaviour {
	public static newTestButton newTest;

	private bool backToStart = false;
	private bool canColide = false;

	public Sprite spr;
	public Sprite spr2;

	void Awake() {
		newTest = this;
	}

	void Update() {
		SpriteRenderer sprRenderer= (SpriteRenderer)GetComponent<Renderer>();
	
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
				getScore.over.isOver = false;
				backToStart = true;
			}
		}

		if (backToStart == true) {
			if (GameObject.Find ("Student").transform.position.x > 0) {
				GameObject.Find ("Student").transform.Translate (-1.0f, 0f, 0f);
			} else if (GameObject.Find ("Student").transform.position.x == 0 || GameObject.Find ("Student").transform.position.x < 0) {
				GameObject.Find ("Student").transform.position = new Vector3(0,0,-10);
				backToStart = false;
				sprRenderer.sprite = spr2;
			}
		}
	}
}
