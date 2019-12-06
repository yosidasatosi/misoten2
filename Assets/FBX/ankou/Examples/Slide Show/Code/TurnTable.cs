using UnityEngine;
using System.Collections;

public class TurnTable : MonoBehaviour {

	public float speed;
	public Vector3 axis = new Vector3(0,1,0);

	// Update is called once per frame
	void Update () {

		transform.Rotate(axis * speed * Time.deltaTime);
	
	}
}
