using UnityEngine;
using System.Collections;

public class SlideShowController : MonoBehaviour {

	const float Native_Width = 1024f;
	const float Native_Height = 768f;

	public Transform[] assets;
	public float slideSpeed = 5;
	public float loopSlideSpeed = 10;

	public GUIStyle slideButtonLeft, slideButtonRight;
	public Texture2D motdLogo;

	private bool inTransition = false;
	private int currentIndex = 0;
	private float actualSpeed; 

	public int CurrentIndex {
		get {
			return currentIndex;
		}
		set {
			if (!inTransition) {
				if (value > assets.Length-1)
				{
					currentIndex = 0;
					actualSpeed = loopSlideSpeed;
				}
				else if (value < 0)
				{
					currentIndex = assets.Length-1;
					actualSpeed = loopSlideSpeed;
				}
				else
				{
					currentIndex = value;
					actualSpeed = slideSpeed;
				}

				StartCoroutine(UpdateSlide());
			}
		}
	}


	void OnGUI () {
		float rx = Screen.width / Native_Width;
		float ry  = Screen.height / Native_Height;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if (GUI.Button (new Rect(0, Native_Height / 2 - 128, 62, 256),"", slideButtonLeft) || Input.GetAxis ("Horizontal") < 0)
			CurrentIndex--;

		if (GUI.Button (new Rect(Native_Width - 62, Native_Height / 2 - 128, 62, 256),"",slideButtonRight) || Input.GetAxis ("Horizontal") > 0)
			CurrentIndex++;	

		GUI.Label(new Rect(10,10,200,200), motdLogo);
	}

	IEnumerator UpdateSlide() {
		if (assets[CurrentIndex] != null)
		{
			inTransition = true;
			yield return StartCoroutine(MoveToPosition(assets[CurrentIndex].position, actualSpeed));
			inTransition = false;
		}
	}


	IEnumerator MoveToPosition(Vector3 targetPosition, float speed)
	{
		while (transform.position != targetPosition) {
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
			yield return 0;
		}
	}


	// Use this for initialization
	void Start () {
		transform.position = assets[0].position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
