using UnityEngine;
using System.Collections;

public class Debugging : MonoBehaviour {
	public string commingAroundTheMountain;
	public Sprite spr;
	
	void OnGUI() {
		if(GUI.Button (new Rect(10,100,100,30),"Save")){
			GameControl.control.save();
		}
		if(GUI.Button (new Rect(10,140,100,30),"Load")){
			GameControl.control.Load();
		}
		if(GUI.Button (new Rect(10,180,100,30),GameControl.control.points.ToString())){
			GameControl.control.points += 10;
		}
	}
}
