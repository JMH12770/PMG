using UnityEngine;
using System.Collections;

public class fadetoscene : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float mohawk = 0;
	public float speed = 0.01f;
	
	private int drawDepth = -1000;

	void Update() {
	    Invoke("mrT",0);
		if ( Input.touchCount == 0) {
			if (transform.position.x > 12) {
				transform.Translate (-0.5f, 0f, 0f);
			} else if (transform.position.x < -10) {
				transform.Translate (+0.5f, 0f, 0f);
			}
		}
	}

	void mrT() {
		if (transform.position.x >= 18) {
			Vector3 temp = new Vector3(0,0,-10);
			GameControl.control.cameraVector = temp;
			Application.LoadLevel("store");
		}
		if (transform.position.x <= -16) {
			Vector3 temp = new Vector3(0,0,-10);
			GameControl.control.cameraVector = temp;
			Application.LoadLevel("customePags");
		}
	}

	void OnGUI () {
		if (transform.position.x >= 12) {
			mohawk = (transform.position.x - 12) / 6;
			mohawk = Mathf.Clamp01 (mohawk);
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, mohawk);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		} else if (transform.position.x <= -10) {
			mohawk = (transform.position.x + 16) / 6;
			mohawk = (mohawk -1)/-1;
			mohawk = Mathf.Clamp01 (mohawk);
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, mohawk);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		}

	}
}