using UnityEngine;
using System.Collections;

public class splashHandler : MonoBehaviour {

	private float timeFade = 1f;

	void Update () {
		timeFade -= Time.deltaTime;

		if (timeFade <= 0) {
			StartCoroutine (toDash ());
		}
	}

	IEnumerator toDash() {
		GameControl.control.cameraVector = new Vector3 (-7, 0, -10);
		float fadeTime = GameObject.Find ("_GM").GetComponent<fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel ("Dashboard");
	}
}
