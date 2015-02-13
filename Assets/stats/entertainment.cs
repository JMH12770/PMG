using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class entertainment : MonoBehaviour {
	public static entertainment control;
	
	private Dictionary<string,float> hardMultiplyers = new Dictionary<string,float> ();
	private Dictionary<string,float> hardBuffs = new Dictionary<string, float> ();
	private List<float[]> softMultiplyers = new List<float[]> ();
	private List<float[]> softBuffs = new List<float[]> ();
	
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		}
		else if (control != this) {
			Destroy (gameObject);
		}
	}
	
	public void addHardBuff (string name, float data) {
		hardBuffs.Add (name, data);
	}
	
	public void addHardMultplyer (string name, float val) {
		hardMultiplyers.Add (name, val);
	}
	
	public void addSoftMultiplyer (float test, float multiplyer) {
		float[] data = new float[] {test, multiplyer};
		softMultiplyers.Add (data);
	}
	
	public void addSoftBuff (float test, float buff) {
		float[] data = new float[] {test, buff};
		softBuffs.Add (data);
	}
	
	public void subtractTests () {
		List<float[]> temp = new List<float[]> ();
		foreach (float[] entry in softMultiplyers) {
			
			entry[0] -= 1;
			if (entry[0] <= 0) {
				temp.Add(entry);
			}
		}
		foreach (float[] entry in temp) {
			softMultiplyers.Remove(entry);
		}
		
		temp = new List<float[]> ();
		foreach (float[] entry in softBuffs) {
			
			entry[0] -= 1;
			if (entry[0] <= 0) {
				temp.Add(entry);
			}
		}
		foreach (float[] entry in temp) {
			softBuffs.Remove(entry);
		}
	}
	
	public void subtractTests (float number) {
		foreach (float[] entry in softMultiplyers) {
			entry[0] -= number;
		}
		foreach (float[] entry in softBuffs) {
			entry[0] -= number;
		}
	}
	
	public float getVal () {
		List<float> temp = new List<float> ();
		float val = 0;
		
		foreach (string buff in hardBuffs.Keys) {
			val += hardBuffs[buff];
		}
		foreach (float[] buff in softBuffs) {
			val += buff[1];
		}
		
		foreach (string mult in hardMultiplyers.Keys) {
			temp.Add (hardMultiplyers[mult]);
		}
		foreach (float[] mult in softMultiplyers) {
			temp.Add (mult[1]);
		}
		
		temp.Sort ();
		foreach (float number in temp) {
			val = val * number;
		}
		
		if (val > 100) {
			val = 100;
		} else if (val < 0) {
			val = 0;
		}
		
		return val;
	}
	
	public Dictionary<string,float> getHardMultiplyers () {
		return hardMultiplyers;
	}
	
	public Dictionary<string,float> getHardBuffs () {
		return hardBuffs;
	}
	
	public List<float[]> getSoftMultiplyers () {
		return softMultiplyers;
	}
	
	public List<float[]> getSoftBuffs () {
		return softBuffs;
	}
	
	public void setHardMultiplyers (Dictionary<string,float> val) {
		hardMultiplyers = val;
	}
	
	public void setHardBuffs (Dictionary<string,float> val) {
		hardBuffs = val;
	}
	
	public void setSoftMultiplyers (List<float[]> val) {
		softMultiplyers = val;
	}
	
	public void setSoftBuffs (List<float[]> val) {
		softBuffs = val;
	}
}
