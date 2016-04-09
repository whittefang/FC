using UnityEngine;
using System.Collections;

public class AIMovementScript : MonoBehaviour {
	public Transform currentTarget;
	public float distanceBuffer = 1, translateSpeed = .05f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentTarget != null) {
			MoveTowardsTarget ();
		}
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
}
