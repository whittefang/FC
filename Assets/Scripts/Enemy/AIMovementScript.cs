using UnityEngine;
using System.Collections;

public class AIMovementScript : MonoBehaviour {
	public Transform currentTarget;
	public float distanceBuffer = 1, distanceBufferMax = 1, translateSpeed = .01f, maxTranslateSpeed = .01f;
	bool isMoving = false, inVortex = false, disabled = false;
	AggroScript AS;
	// Use this for initialization
	void Awake () {
		AS = GetComponent<AggroScript> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!disabled) {
			if (!isMoving && (currentTarget != null) && ((Mathf.Abs (currentTarget.position.x - transform.position.x) > 1f + distanceBuffer) || (Mathf.Abs (currentTarget.position.y - transform.position.y) > 1f + distanceBuffer))) {
				StopAllCoroutines ();
				StartCoroutine (MoveWrapper ());
			}
		}
	}
	IEnumerator MoveWrapper(){
		isMoving = true;
		while ((Mathf.Abs (currentTarget.position.x - transform.position.x) > .1f + distanceBuffer) || (Mathf.Abs (currentTarget.position.y - transform.position.y) > .1f+ distanceBuffer)) {
			MoveTowardsTarget ();
			yield return null;
		}
		isMoving = false;
	}
	void MoveTowardsTarget(){
		Vector3 newPosition = transform.position;

		// check if x is bigger or smaller
		if (currentTarget.position.x -  transform.position.x >  distanceBuffer)  {
			newPosition.x += translateSpeed;
		} else if (currentTarget.position.x -  transform.position.x <  -distanceBuffer){
			newPosition.x -= translateSpeed;
		}

		// check if y is bigger or smaller
		if (currentTarget.position.y -  transform.position.y >  distanceBuffer)  {
			newPosition.y += translateSpeed;
		} else if (currentTarget.position.y -  transform.position.y <  -distanceBuffer){
			newPosition.y -= translateSpeed;
		}

		transform.position = newPosition;
	}

	public void SetTarget(Transform newTarget){
		if (!inVortex) {
			currentTarget = newTarget;
		}
	}
	public void StartVortex(Transform position){

		StopAllCoroutines ();
		isMoving = true;
		inVortex = true;
		currentTarget = position;
		distanceBuffer = .1f;
		translateSpeed = .005f;
		StartCoroutine (VortextMovement ());
	}
	public void StopVortex(){
		StopAllCoroutines ();
		isMoving = false;
		inVortex = false;
		translateSpeed = maxTranslateSpeed;
		distanceBuffer = distanceBufferMax;
		currentTarget = AS.GetTarget ();
	}
	IEnumerator VortextMovement(){
		while ((Mathf.Abs (currentTarget.position.x - transform.position.x) > .1f ) || (Mathf.Abs (currentTarget.position.y - transform.position.y) > .1f)) {
			MoveTowardsTarget ();
			yield return null;
		}


	}
	public void SetMovementEnabled(bool state = true){
		if (!state) {
			StopAllCoroutines ();
		}
		disabled = state;
		inVortex = false;
		isMoving = false;
	}
}
