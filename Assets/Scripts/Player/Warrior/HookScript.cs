using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {
	public GameObject Owner;
	Transform hookedTrans;
	bool player = false, canGrab = false;
	public bool objectHooked = false;
	PlayerMovementScript currentPlayer;
	AIMovementScript currentAi;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (objectHooked) {
			hookedTrans.position = transform.position;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (canGrab && other.gameObject != Owner) {
			if (other.tag == "Enemy") {
				hookedTrans = other.transform;
				currentAi = other.GetComponent<AIMovementScript> ();
				currentAi.SetMovementEnabled (false);
				player = false;
				canGrab = false;
				objectHooked = true;
			} else if (other.tag == "Player") {
				hookedTrans = other.transform;
				currentPlayer = other.GetComponent<PlayerMovementScript> ();
				currentPlayer.SetSpeed (0);
				player = true;
				canGrab = false;
				objectHooked = true;
			}
		}
	}
	public void EnableHook(){
		canGrab = true;
	}
	public void ResetMovement(){
		if (player && objectHooked) {
			currentPlayer.ResetSpeed ();
		} else if (objectHooked){
			currentAi.SetMovementEnabled (true);
		}
		objectHooked = false;
	}

}
