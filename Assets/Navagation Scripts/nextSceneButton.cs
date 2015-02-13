using UnityEngine;
using System.Collections;

public class nextSceneButton : MonoBehaviour {
	
	public string commingAroundTheMountain;
	public Vector3 cameraControl = new Vector3 (0, 0, -10);
	public Sprite spr;
	public bool isEnabled = true;

	private bool canColide = false;

	void Update()
	{
		if (isEnabled == true){
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
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
				sprRenderer.sprite = spr;
				GameControl.control.cameraVector = cameraControl;
				StartCoroutine(toScene());
			}
		}
		}
	}
	
	IEnumerator toScene() {
		float fadeTime = GameObject.Find ("_GM").GetComponent<fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (commingAroundTheMountain);
	}
}
