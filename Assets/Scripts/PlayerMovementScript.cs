using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {
	Rigidbody2D RB;
	public float speed;
	// Use this for initialization
	void Awake () {
		RB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ProcessMovement(float x,  float y){
		if (x < .05f) {
			x = 0;
		}
		if (y < .05f) {
			y = 0;
		}
		RB.velocity = new Vector2 (x*speed, y*speed);
	}
}
