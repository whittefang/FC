using UnityEngine;
using System.Collections;

public class AIMovementScript : MonoBehaviour {
	public Transform currentTarget;
	public float distanceBuffer = 1, translateSpeed = .01f;
	bool isMoving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ( !isMoving && (currentTarget != null) && ((Mathf.Abs (currentTarget.position.x - transform.position.x) > 1f + distanceBuffer) || (Mathf.Abs (currentTarget.position.y - transform.position.y) > 1f+ distanceBuffer))) {
			StopAllCoroutines ();
			StartCoroutine(MoveWrapper());
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
		currentTarget = newTarget;
	}
}
