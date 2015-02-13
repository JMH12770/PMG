using UnityEngine;
using System.Collections;

public class fading : MonoBehaviour {

	public Texture2D fadeOutTexture; // the black
	public float fadeSpeed = 0.8f;   // the fade speed

	private int drawDepth = -1000;   // Textures draw order in hierarchy
	private float alpha = 1.0f;
	private int fadeDir = -1;

	//logic for fade in and out
	void OnGUI () {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	// set fade direction
	public float BeginFade (int direction) {
		fadeDir = direction;
		return (fadeSpeed);
	}

	// onlevel loaded
	void OnLevelWasLoaded () {
		BeginFade (-1);
	}
}
