using UnityEngine;
using System.Collections;

public class VampireSquidController : MonoBehaviour {

	const float Native_Width = 1024f;
	const float Native_Height = 768f;

	public Material squidMaterial;

	public float swimUpForce = 15;

	public float lateralForce = 5;

	public float lateralDampening = 1;

	public float photoforeMaxIntensity = 1;

	public GameObject squirtParticleSystem;

	public bool swimming = false;

	public Vector3 inkSquirtPosition;

	public GUIStyle helpText;

	Vector2 oldDirection, newDirection, smoothedDirection;

	bool swimmingUp = false;

	bool glowing = false;

    

	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -0.2f, 0);
        
	}

	void Update () {

		if (Input.GetButton ("Fire1"))
		{
			glowing = true;

		}
		else
		{
			glowing = false;

		}
			
		
		if (Input.GetButtonUp ("Fire2"))
		{
			Debug.Log ("Squirting");
			anim.SetTrigger("Squirt");
			//Squirt();
		}

	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector2 inputDir = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		if (inputDir.magnitude > 0.1f) swimming = true;
		else
			swimming = false;

		oldDirection = smoothedDirection;
		newDirection = inputDir;
		smoothedDirection = Vector2.Lerp(oldDirection, newDirection, Time.deltaTime * lateralDampening);

		if (Input.GetKey (KeyCode.Space))
		{
			swimmingUp = true;
			GetComponent<Rigidbody>().AddForce(Vector3.up * swimUpForce);

		}
		else
			swimmingUp = false;


		anim.SetFloat ("HorizontalDirection", smoothedDirection.x);
		anim.SetFloat ("VerticalDirection", smoothedDirection.y);
		anim.SetBool("SwimmingUp", swimmingUp);
		anim.SetBool ("Swimming", swimming);
		anim.SetBool("Glowing", glowing);

		GetComponent<Rigidbody>().AddForce (new Vector3(-inputDir.x, 0, -inputDir.y) * lateralForce);
	
	}


	void SquirtInk()
	{
		Debug.Log ("Imma Squirtin");
		if (squirtParticleSystem)
		{
			GameObject squirtParticles = (GameObject) Instantiate(squirtParticleSystem, transform.position + inkSquirtPosition, Quaternion.identity);
			squirtParticles.transform.parent = transform;
		}
			
	}

	void OnDrawGizmosSelected()
	{

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position + inkSquirtPosition, 1f);
	}

	void OnGUI() {
		float rx = Screen.width / Native_Width;
		float ry  = Screen.height / Native_Height;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		
		GUI.Label(new Rect(0,Native_Height-30,Native_Width,30), "[WASD] moves squid Laterally, [SPACE] to swim up, [Fire1] to glow, [Fire2] to squirt", helpText);
	}


}
