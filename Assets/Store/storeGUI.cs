using UnityEngine;
using System;
using System.Collections;

public class storeGUI : MonoBehaviour {
	public static storeGUI control;

	public GUIStyle storeList;
	public Vector2 scrollPosition = Vector2.zero;
	public Texture2D background;
	public GUIStyle buttonStyle;
	public GUIStyle textStyle;
	public float speed;

	private string[] levelsOwned = new string[10];
	private Texture2D current;
	private Texture2D currentRender;
	private bool renderInfo;
	private string flaverText;
	private string currentItem;
	private int currentID;

	void Awake () {
		control = this;
	}

	void Start () {
		levelsOwned = storeControl.control.levelOwned.Split (',');
	}

	void Update () {
		if (Input.touchCount > 0)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				
				if (Input.GetTouch(0).phase == TouchPhase.Moved) {
					Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
					scrollPosition.y += (touchDeltaPosition.y * speed * (Time.deltaTime*20));
				}
			}
		}
	}

	void OnGUI () {
		float modify = 1f;
		textStyle.fontSize = Screen.height / 17;
		GUI.Box (new Rect (Screen.width / 25, Screen.height / 14, (Screen.width / 25) * 12, (Screen.height / 14) * 12),"Store");

		GUI.BeginScrollView(new Rect (Screen.width / 25, Screen.height / 14*modify, (Screen.width / 25) * 12, (Screen.height / 14) * 12),
		                    scrollPosition, new Rect(0,0,(Screen.width/50)*14,(Screen.height/28)*91));

		StartCoroutine (renderGUI ());

		GUI.EndScrollView ();

		if (renderInfo == true) {
			int price = System.Convert.ToInt32(getPrice(currentItem));

			GUI.Box (new Rect ((Screen.width / 25) * 14, (Screen.height / 14) * 8, (Screen.width / 25) * 10, (Screen.height / 14) * 5), background);
			renderFlavertext(price.ToString());

			StartCoroutine(renderImage());


			if (Int32.Parse (levelsOwned[currentID]) < 4){
				if (GUI.Button (new Rect ((Screen.width/25)*14,(Screen.height/14)*12,(Screen.width/25)*10,(Screen.height/14)),"Upgrade")) {
					if (GameControl.control.points >= price) {
						GameControl.control.points -= price;
						GameControl.control.save ();

						levelsOwned[currentID] = (Int32.Parse (levelsOwned[currentID]) + 1 ).ToString();
						storeControl.control.levelOwned = string.Join(",",levelsOwned);
						storeControl.control.Save ();
	
						int mrT = Int32.Parse(levelsOwned[currentID]);
						if (mrT > 3){mrT = 3;}

						flaverText = (storeControl.control.descriptions[currentItem].ToString()).Split('|')[mrT];
						int ownedLevel = Int32.Parse(levelsOwned[currentID]);
						ownedLevel++;

						if (ownedLevel > 4){
							ownedLevel = 4;
						}

						currentRender = Resources.Load<Texture2D>("Dev Art/"+currentItem+"/"+currentItem+""+ownedLevel.ToString());
						StartCoroutine(renderImage ());
					}
				}
			}
		}

	}

	IEnumerator renderGUI () {
		int buttonPos = 1;
		float texturePos = 3;
		float labelPos = 7;
		int i = 0;
		
		foreach (string name in storeControl.control.names)
		{
			if(GUI.Button (new Rect( Screen.width/50, (Screen.height/28)*buttonPos, (Screen.width/50)*22, (Screen.height/28)*8),background,buttonStyle)) {
				int ownedLevel = Int32.Parse((levelsOwned[i]));
				ownedLevel++;

				if (ownedLevel > 4){
					ownedLevel = 4;
				}

				currentItem = name;
				currentID = i;
				flaverText = (storeControl.control.descriptions[name].ToString()).Split ('|')[(ownedLevel-1)];
				currentRender = Resources.Load<Texture2D>("Dev Art/"+name+"/"+name+""+ownedLevel.ToString());
				renderInfo = true;
			}
			
			current = Resources.Load<Texture2D>("Dev Art/"+name+"/"+name+""+levelsOwned[i]);
			GUI.DrawTexture (new Rect ((Screen.width / 125) * 3, (Screen.height / 56) * texturePos, (Screen.height / 56) * 14, (Screen.height / 56) * 14),
			                 current, ScaleMode.StretchToFill, true, 10.0f);
			
			GUI.Label (new Rect ((Screen.width / 125) * 20, (Screen.height / 56) * labelPos, (Screen.height / 56) * 14, (Screen.height / 56) * 14),
			           "Upgade", textStyle);
			
			buttonPos += 9;
			texturePos += 18.5f;
			labelPos += 18.5f;
			
			i++;
		}
		yield return null;
	}

	IEnumerator renderImage() {
		GUI.DrawTexture (new Rect ((Screen.width / 25) * 17, (Screen.height / 14) * 2, (Screen.width / 25) * 4, (Screen.width / 25) * 4), currentRender);

		yield return null;
	}

	void renderFlavertext (string price) {
		GUI.Label (new Rect ((Screen.width/75)*43,(Screen.height/28)*17,(Screen.width/75)*29,(Screen.height/28)*8),
		           flaverText+" "+price, textStyle);
	}

	double getPrice(string name) {
		double thisprice;
		double thisLevel = System.Convert.ToDouble(Int32.Parse (levelsOwned [currentID]));

		thisprice = System.Convert.ToDouble(Int32.Parse (storeControl.control.prices [name].ToString ())) * Math.Pow(System.Convert.ToDouble(storeControl.control.multiplyer[name]), thisLevel);
		return thisprice;
	}
}
