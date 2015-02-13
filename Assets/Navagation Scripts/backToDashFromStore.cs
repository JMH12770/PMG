using UnityEngine;
using System.Collections;

public class backToDashFromStore : MonoBehaviour {

	public Texture2D fadeOutTexture;
	private int drawDepth = -1000; 
	public float mohawk = 0;

	void Update() {
		Invoke("limits",0);
		if ( Input.touchCount == 0) {
			if (transform.position.x > 4) {
				transform.Translate (-0.5f, 0f, 0f);
			} else if (transform.position.x < -4) {
				transform.Translate (+0.5f, 0f, 0f);
			}
		}
	}
	void limits() {
		if (transform.position.x <= -10) {
			Vector3 temp = new Vector3(12,0,-10);
			GameControl.control.cameraVector = temp;
			Application.LoadLevel("Dashboard");
		}
		if (transform.position.x >= 10) {
			Vector3 temp = new Vector3(0,0,-10);
			GameControl.control.cameraVector = temp;
			Application.LoadLevel("customePags");
		}
	}

	void OnGUI () {
		if (transform.position.x >= 4) {
			mohawk = (transform.position.x - 4) / 6;
			mohawk = Mathf.Clamp01 (mohawk);
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, mohawk);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		} else if (transform.position.x <= -4) {
			mohawk = (transform.position.x + 10) / 6;
			mohawk = (mohawk -1)/-1;
			mohawk = Mathf.Clamp01 (mohawk);
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, mohawk);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		} else {
			mohawk = 0;
			mohawk = Mathf.Clamp01 (mohawk);
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, mohawk);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		}
	}

}
