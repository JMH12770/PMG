using UnityEngine;
using System.Collections;

public class displayPoints : MonoBehaviour {

	// Use this for initialization
	void OnGUI() {
		gameObject.GetComponent<TextMesh> ().text = "Points: "+GameControl.control.points+"\nLifetime: "+GameControl.control.lifePoints;
	}
}
