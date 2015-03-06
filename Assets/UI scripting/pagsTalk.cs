using UnityEngine;
using System.Collections;

public class pagsTalk : MonoBehaviour {

	private bool canColide = false;
	private bool conversation = false;

	void Update () {
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved && canColide == true) {
			if (Input.GetTouch(0).deltaPosition.x > 0.3f){
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
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && canColide == true && conversation != true)
		{
			canColide = false;
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				cameraScroll.cameraControl.enabled = false;
				conversation = true;

				string[] temp = new string[] {"What do you want?",GameControl.control.getPagsName (),"Can I have my\n  test back?","High Five!","Nothing"};
				conversationHandler.control.conversation(temp);
			}
		}

		if (conversation == true) {
			if (conversationHandler.control.replyReady == true) {
				string reply = conversationHandler.control.getResponce();

				string[] temp;
				switch (reply)
				{
				case "High Five!":
					temp = new string[] {"Sit Down",GameControl.control.getPagsName (),"..."};
					conversationHandler.control.conversation(temp);
					break;
				case "Can I have my\n  test back?":
					temp = new string[] {"No",GameControl.control.getPagsName ()};
					conversationHandler.control.conversation(temp);
					break;
				case "No":
					temp = new string[] {"PLEASE... I realy\nWant to get my|Test Back!","mrWhite"};
					conversationHandler.control.conversation(temp);
					break;
				case "PLEASE... I realy\nWant to get my":
					temp = new string[] {"No.",GameControl.control.getPagsName ()};
					conversationHandler.control.conversation(temp);
					break;
				case "No.":
					temp = new string[] {"...","me"};
					conversationHandler.control.conversation(temp);
					break;
				case "Nothing":
					cameraScroll.cameraControl.enabled = true;
					conversationHandler.control.reset();
					conversation = false;
					break;
				case "...":
					cameraScroll.cameraControl.enabled = true;
					conversationHandler.control.reset();
					conversation = false;
					break;
				default:
					cameraScroll.cameraControl.enabled = true;
					conversationHandler.control.reset();
					conversation = false;
					break;
				}
			}
		}
	}
}
