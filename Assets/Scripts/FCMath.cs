using UnityEngine;
using System.Collections;

public class FCMath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	static public float LockDirection(float x, float y){
		float newAngle = 0;
		if (x > .5f && y >-.5f && y < .5f) {
			newAngle = 0;
		}else if (x > .5f && y >.5f ) {
			newAngle = 45;
		}else if (x > .5f && y < -.5f) {
			newAngle = -45;
		}else if (x < -.5f && y >.5f) {
			newAngle = 135;
		}else if (x < -.5f && y <-.5f) {
			newAngle = -135;
		}else if (x < -.5f && y >-.5f && y < .5f) {
			newAngle = 180;
		}

		return newAngle;
	}
	static public Vector2 LockDirectionV2(float x, float y){
		Vector2 direction = Vector2.zero;
		if (x > .5f) {
			direction.x = 1;
		}else if  (x < -.5f) {
			direction.x = -1;
			
		}else {
			direction.x = 0;
		}

		if (y > .5f) {
			direction.y = 1;
		}else if  (y < -.5f) {
			direction.y = -1;

		}else {
			direction.y = 0;
		}
		return direction;
	}
}
