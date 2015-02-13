using UnityEngine;
using System.Collections;

public class scrollBack : MonoBehaviour {
	private bool canColide = false;
	private bool isPanning = false;

	public Sprite spr;
	public Sprite spr2;

	void Update()
	{
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
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
				sprRenderer.sprite = spr;
				isPanning = true;
			}
		}else {sprRenderer.sprite = spr2;}
		
		if (isPanning == true && GameObject.Find ("Student").transform.position.x < 0) {
			GameObject.Find ("Student").transform.Translate (1f, 0f, 0f);
		} else {isPanning = false;}
	}
}
