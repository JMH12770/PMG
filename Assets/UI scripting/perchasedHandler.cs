using UnityEngine;
using System.Collections;

public class perchasedHandler : MonoBehaviour {

	public Sprite spr;
	public int listingID;
	public int price = 0;
	public string achievement;
	private bool canColide;

	void Start () {
		checkOwnership (true);
	}

	void Update () {
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && canColide == true) {
			if (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) > 1f){
				canColide = false;
			}
		
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				canColide = true;
			} else { canColide = false;}
		}
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				checkOwnership (false);
			}
		}
	}

	void checkOwnership (bool lookup) {
		string[] costumeList = GameControl.control.isOwnedCostume.Split (',');
		
		if (lookup == true){
			if (costumeList[listingID] == "1") {
				SpriteRenderer sprRenderer = (SpriteRenderer)GetComponent<Renderer>();
				sprRenderer.sprite = spr;
			}
		}

		if (lookup == false) {
			if (costumeList[listingID] == "1"){
				pagsHandler.pags.updatePags(listingID);
				GameControl.control.currentpags = listingID;
				GameControl.control.save();
			}
			else if (costumeList[listingID] == "0"){
				if (GameControl.control.points >= price) {
					GameControl.control.points -= price;
					costumeList[listingID] = "1";
					string writeCostumeList = string.Join(",", costumeList);
					GameControl.control.isOwnedCostume = writeCostumeList;
					GameControl.control.currentpags = listingID;
					GameControl.control.save ();
					pagsHandler.pags.updatePags(listingID);
					checkOwnership(true);
					if (achievement != ""){
						Social.ReportProgress(achievement, 100.0f, (bool success2) => {
						if (success2){
						}
					});
					}
				}
			}
		}
	}
}
