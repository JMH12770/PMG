using UnityEngine;
using System.Collections;
using System.IO;

public class optionsGUIHandler : MonoBehaviour {
	public float hSliderValue = 5.0f;

	void Start () {
		hSliderValue = PlayerPrefs.GetFloat("randomEventTimer");
	}

	void OnGUI () {
		hSliderValue = Mathf.Round (GUI.HorizontalSlider (new Rect ((Screen.width/10), ((Screen.height/12)*6), (Screen.width/10)*5, Screen.height / 14), hSliderValue,0.0f,10.0f));
		GUI.Label(new Rect ((Screen.width/10)*5, (Screen.height/12)*6, (Screen.width/10)*5, Screen.height / 14), "Random event every "+hSliderValue+" minouts");
		if (GUI.Button (new Rect ((Screen.width / 10) * 5, (Screen.height / 10) * 8, (Screen.width / 10) * 2, (Screen.height / 14) * 2), "Save")) {
			PlayerPrefs.SetFloat("randomEventTimer", hSliderValue);
			PlayerPrefs.Save();
			StartCoroutine(toScene());
		}
		if (GUI.Button (new Rect ((Screen.width / 10) * 2, (Screen.height / 10) * 8, (Screen.width / 10) * 2, (Screen.height / 14) * 2), "CLEAR SAVES")) {
			DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Application.persistentDataPath);
			
			foreach (FileInfo file in downloadedMessageInfo.GetFiles())
			{
				Debug.Log (file);
				file.Delete(); 
			}
			Application.Quit();
		}
	}

	IEnumerator toScene() {
		float fadeTime = GameObject.Find ("_GM").GetComponent<fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		GameControl.control.cameraVector = new Vector3 (-7,0,-10);
		Application.LoadLevel ("Dashboard");
	}
}
