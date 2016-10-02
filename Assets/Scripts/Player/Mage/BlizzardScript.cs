using UnityEngine;
using System.Collections;

public class BlizzardScript : MonoBehaviour {
	CircleCollider2D trigger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable(){
		if (trigger == null) {
			trigger = GetComponent<CircleCollider2D> ();
		}
		StartCoroutine (AreaDamage ());	
	}
	IEnumerator AreaDamage(){
		for (int x = 0; x < 120; x++) {
			trigger.enabled = true;
			yield return null;
			trigger.enabled = false;
		}
		gameObject.SetActive (false);
	}
}
