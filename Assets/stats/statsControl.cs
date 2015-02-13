using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class statsControl : MonoBehaviour {
	public static statsControl control;
	
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		}
		else if (control != this) {
			Destroy (gameObject);
		}
	}

	public float calculateMultiplyer () {
		float multiplyer = 1;
		float[] values = new float[] {
						satisfaction.control.getVal (),
						annoyence.control.getVal (),
						entertainment.control.getVal (),
						happyness.control.getVal ()
				};
		foreach (float value in values) {
			multiplyer = multiplyer * ((2*(value/1000))+1);
		}

		return multiplyer;
	}

	// Save/Load Handlers
	public void save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/irrelevent.dat");

		StatsData data = new StatsData ();
		data.hardSatisfactionMultiplyers = satisfaction.control.getHardMultiplyers ();
		data.hardSatisfactionBuffs = satisfaction.control.getHardBuffs ();
		data.softSatisfactionMultiplyers = satisfaction.control.getSoftMultiplyers ();
		data.softSatisfactionBuffs = satisfaction.control.getSoftBuffs ();

		data.hardAnnoyenceMultiplyers = annoyence.control.getHardMultiplyers ();
		data.hardAnnoyenceBuffs = annoyence.control.getHardBuffs ();
		data.softAnnoyenceMultiplyers = annoyence.control.getSoftMultiplyers ();
		data.softAnnoyenceBuffs = annoyence.control.getSoftBuffs ();

		data.hardEntertainmentMultiplyers = entertainment.control.getHardMultiplyers ();
		data.hardEntertainmentBuffs = entertainment.control.getHardBuffs ();
		data.softEntertainmentMultiplyers = entertainment.control.getSoftMultiplyers ();
		data.softEntertainmentBuffs = entertainment.control.getSoftBuffs ();

		data.hardHappynessMultiplyers = happyness.control.getHardMultiplyers ();
		data.hardHappynessBuffs = happyness.control.getHardBuffs ();
		data.softHappynessMultiplyers = happyness.control.getSoftMultiplyers ();
		data.softHappynessBuffs = happyness.control.getSoftBuffs ();


		bf.Serialize (file, data);
		file.Close();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/irrelevent.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/irrelevent.dat", FileMode.Open);
			StatsData data = (StatsData)bf.Deserialize(file);
			file.Close();

			satisfaction.control.setHardMultiplyers(data.hardSatisfactionMultiplyers);
			satisfaction.control.setHardBuffs(data.hardSatisfactionBuffs);
			satisfaction.control.setSoftMultiplyers(data.softSatisfactionMultiplyers);
			satisfaction.control.setSoftBuffs(data.softSatisfactionBuffs);
			
			annoyence.control.setHardMultiplyers(data.hardAnnoyenceMultiplyers);
			annoyence.control.setHardBuffs(data.hardAnnoyenceBuffs);
			annoyence.control.setSoftMultiplyers(data.softAnnoyenceMultiplyers);
			annoyence.control.setSoftBuffs(data.softAnnoyenceBuffs);
			
			entertainment.control.setHardMultiplyers(data.hardEntertainmentMultiplyers);
			entertainment.control.setHardBuffs(data.hardEntertainmentBuffs);
			entertainment.control.setSoftMultiplyers(data.softEntertainmentMultiplyers);
			entertainment.control.setSoftBuffs(data.softEntertainmentBuffs);
			
			happyness.control.setHardMultiplyers(data.hardHappynessMultiplyers);
			happyness.control.setHardBuffs(data.hardHappynessBuffs);
			happyness.control.setSoftMultiplyers(data.softHappynessMultiplyers);
			happyness.control.setSoftBuffs(data.softHappynessBuffs);

		}
	}
}

[Serializable]
class StatsData
{
	public Dictionary<string,float> hardSatisfactionMultiplyers;
	public Dictionary<string,float> hardSatisfactionBuffs;
	public List<float[]> softSatisfactionMultiplyers;
	public List<float[]> softSatisfactionBuffs;

	public Dictionary<string,float> hardAnnoyenceMultiplyers;
	public Dictionary<string,float> hardAnnoyenceBuffs;
	public List<float[]> softAnnoyenceMultiplyers;
	public List<float[]> softAnnoyenceBuffs;

	public Dictionary<string,float> hardEntertainmentMultiplyers;
	public Dictionary<string,float> hardEntertainmentBuffs;
	public List<float[]> softEntertainmentMultiplyers;
	public List<float[]> softEntertainmentBuffs;

	public Dictionary<string,float> hardHappynessMultiplyers;
	public Dictionary<string,float> hardHappynessBuffs;
	public List<float[]> softHappynessMultiplyers;
	public List<float[]> softHappynessBuffs;
}
