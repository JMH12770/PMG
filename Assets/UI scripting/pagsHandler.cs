using UnityEngine;
using System.Collections;

public class pagsHandler : MonoBehaviour {
	public static pagsHandler pags;
	
	// Use this for initialization
	void Start () {
		pags = this;
		updatePags (GameControl.control.currentpags);
	}

	public void updatePags (int listingID) {
		Sprite currentPags = getPags (listingID);

		SpriteRenderer sprRenderer = (SpriteRenderer)GetComponent<Renderer>();
		sprRenderer.sprite = currentPags;
	}

	Sprite getPags(int pagsID) {
		Sprite renderDir;
		switch (pagsID)
		{
		case 0:
			renderDir = Resources.Load<Sprite> ("Dev Art/Pagses/Pags");
			break;
		case 1:
			renderDir = Resources.Load<Sprite> ("Dev Art/Pagses/princessPags");
			break;
		case 2:
			renderDir = Resources.Load<Sprite> ("dev Art/Pagses/spacePags");
			break;
		default:
			renderDir = Resources.Load<Sprite> ("Dev Art/Pagses/Pags");
			break;
		}
		return renderDir;
	}
}
