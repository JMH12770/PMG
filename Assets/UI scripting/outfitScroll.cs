using UnityEngine;
using System.Collections;
using System.Linq;

public class outfitScroll : MonoBehaviour {
	public static outfitScroll canScroll;

	private float speed = 0.1f;

	void Start () {
		canScroll = this;
	}
	
	void Update () {
		if (Input.touchCount > 0)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (Physics2D.OverlapPointAll(touchPos).Contains (collider2D))
			{
				cameraScroll.cameraControl.isEnabled = false;

				if (Input.GetTouch(0).phase == TouchPhase.Moved) {

					Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

					GameObject.Find ("listContent").transform.Translate (0, touchDeltaPosition.y * speed * (Time.deltaTime*20), 0);
				}
			} else {cameraScroll.cameraControl.isEnabled = true;}
		}
	}
}
