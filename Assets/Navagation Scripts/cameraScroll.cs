using UnityEngine;
using System.Collections;

public class cameraScroll : MonoBehaviour {
	public static cameraScroll cameraControl;

	public bool isEnabled = true;
	public float speed = 0.8f;

	void Awake () {
		cameraControl = this;
	}

	void Update () {
		if (isEnabled == true && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			
			// Move object across XY plane
			transform.Translate (-touchDeltaPosition.x * speed * (Time.deltaTime*20), 0, 0);
		}
	}
}
