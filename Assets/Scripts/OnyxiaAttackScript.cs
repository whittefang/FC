using UnityEngine;
using System.Collections;


public class OnyxiaAttackScript : MonoBehaviour {
	public GameObject swingHitbox, tailWhipHitbox, DeepBreathHitbox, fireBreathHitbox;
	int fireBreathCooldown = 60;

	AggroScript AS;


	// Use this for initialization
	void Start () {
		AS = GetComponent<AggroScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SlashAttack(){
		// animation for swing
		yield return new WaitForSeconds (2f);
		//attack is active
		swingHitbox.SetActive (true);
		yield return null;
		// attack is in recovery
		swingHitbox.SetActive (false);

	}

	IEnumerator TailWhip(){
		yield return new WaitForSeconds (1f);
		tailWhipHitbox.SetActive (true);
		yield return null;
		tailWhipHitbox.SetActive (false);
	}

	IEnumerator FireBreath(){
		fireBreathCooldown = 60;
		StartCoroutine( StartFireBreathCoolDown());
		yield return new WaitForSeconds (1);
		// calcualte firebreath hit area

	}

	IEnumerator DeepBreath(){
		// move to the middle of room
		// alert attack
		// do attack
		DeepBreathHitbox.SetActive (true);
		yield return null;
		DeepBreathHitbox.SetActive (false);
		// return to ground

	}

	IEnumerator StartFireBreathCoolDown(){
		while (fireBreathCooldown > 0){
			yield return new WaitForSeconds(1);
			fireBreathCooldown--;
		}
		yield return null;

	}

	// this function will evaluate what attack to do next and start it
	void ExecuteAttack(){
		// always firebreath if its off cooldown
		if (fireBreathCooldown <= 0) {
			StartCoroutine (FireBreath());
		 
		}else if (Vector3.Distance( AS.GetTargetTransform().position, transform.position ) < 2){
			int rand = Random.Range (0, 100);
			if (rand <= 75) {
				StartCoroutine (SlashAttack());
			} else {
				StartCoroutine (TailWhip());
			}

		}
	}
}
