using UnityEngine;
using System.Collections;

public class testButtons : MonoBehaviour {
	public string whichTest;
	public Sprite spr;
	public Sprite spr2;
	public int timeLimit = 60;
	public int pointModifyer = 10;
	
	private bool canColide = false;

	void Update()
	{
		SpriteRenderer sprRenderer= (SpriteRenderer)GetComponent<Renderer>();
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
				sprRenderer.sprite = spr;
				generateTest.generate.timeLimit = timeLimit;
				getScore.over.pointModifyer = pointModifyer;
				generateTest.generate.timeUp = false;
				generateTest.generate.testData = new Hashtable();
				generateTest.generate.questionData = new string[4];
				generateTest.generate.i = 0;
				generateTest.generate.newTest(whichTest);
			}
		}else {sprRenderer.sprite = spr2;}
	}
}
