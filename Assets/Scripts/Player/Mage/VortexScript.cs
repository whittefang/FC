using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VortexScript : MonoBehaviour {
	CircleCollider2D trigger;
	List<AIMovementScript> affected;
	// Use this for initialization
	void Awake () {
		affected = new List<AIMovementScript> ();
		trigger = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable(){
		StartCoroutine(DealDamage());
	}
	IEnumerator DealDamage(){
		for (int x = 0; x < 180; x++) {
			trigger.enabled = true;
			yield return null;
			trigger.enabled = false;
		}
		Debug.Log ("end");
		foreach (AIMovementScript x in affected) {
			x.StopVortex ();
		}
		affected.Clear();
		gameObject.SetActive (false);

	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			AIMovementScript tmp = other.GetComponent<AIMovementScript> ();
			if (!affected.Contains (tmp)) {
				affected.Add (tmp);
				tmp.StartVortex (transform);
			}
		}
	}
}
