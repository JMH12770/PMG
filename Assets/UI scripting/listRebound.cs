using UnityEngine;
using System.Collections;

public class listRebound : MonoBehaviour {

	private Vector2 startPos;
	void Start () {
		startPos = transform.position;
	}

	void Update () {
		if (transform.position.y <= startPos.y && Input.touchCount == 0) {
			transform.Translate (0, 0.8f, 0);
		}
	}
}
