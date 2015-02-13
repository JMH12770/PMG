using UnityEngine;
using System.Collections;

public class CameraSet : MonoBehaviour {

	void OnLevelWasLoaded () {
		gameObject.transform.position = GameControl.control.cameraVector;
		Debug.Log ("Student x position:" + GameObject.Find ("Student").transform.position, gameObject);
	}
}
