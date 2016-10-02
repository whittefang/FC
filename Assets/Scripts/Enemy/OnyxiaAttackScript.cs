using UnityEngine;
using System.Collections;


public class OnyxiaAttackScript : MonoBehaviour {
	public GameObject swingHitbox, tailWhipHitbox, DeepBreathHitbox, fireBreathHitbox;
	int fireBreathCooldown = 60;
	public PolygonCollider2D FireHitbox;
	public Mesh fireMesh;
	public MeshFilter MF;
	AggroScript AS;


	// Use this for initialization
	void Start () {
		MF.mesh = fireMesh = new Mesh ();
		AS = GetComponent<AggroScript> ();
		//ExecuteAttack ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			BreathHitbox ();
		}
	}

	IEnumerator SlashAttack(){
		// animation for swing
		Debug.Log("slashattack");
		yield return new WaitForSeconds (2f);
		//attack is active
		swingHitbox.SetActive (true);
		yield return null;
		// attack is in recovery
		swingHitbox.SetActive (false);
		ExecuteAttack ();
	}

	IEnumerator TailWhip(){
		Debug.Log("tailattack");
		yield return new WaitForSeconds (1f);
		tailWhipHitbox.SetActive (true);
		yield return null;
		tailWhipHitbox.SetActive (false);
		ExecuteAttack ();
	}

	IEnumerator FireBreath(){
		Debug.Log("firebreath");
		fireBreathCooldown = 60;
		StartCoroutine( StartFireBreathCoolDown());
		yield return new WaitForSeconds (1);
		fireBreathHitbox.SetActive (false);
		// calcualte firebreath hit area
		ExecuteAttack();

	}
	void BreathHitbox(){
		

		Vector3[] newPoints = new Vector3[21];
		Vector2[] newPointsv2 = new Vector2[21];
		float angle = transform.eulerAngles.z;
		angle = (angle - 30) * Mathf.Deg2Rad;
		int[] triangles = new int[60];
		for (int x = 0; x < 20; x++) {
			// include a layer mask to only hit environment and shield
			Debug.Log (angle * Mathf.Rad2Deg + " , " + (x*3));
			angle = ((angle*Mathf.Rad2Deg) + 3) * Mathf.Deg2Rad ; 

			Vector2 direction = new Vector3(Mathf.Cos(angle) , Mathf.Sin(angle));			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 6, 1<<9 );
			Debug.DrawRay(transform.position,  direction);
			// check if null
			if (hit.collider != null) {
				// determine shortened distance if not null
				newPoints [x] = (Vector3)hit.point - transform.position;
				newPointsv2 [x] = (Vector2)newPoints [x];
			} else {
				Debug.Log ("raymiss");
				// else determine full distance
				newPoints[x] =  direction * 6;
				newPointsv2 [x] = (Vector2)newPoints [x];
			}

			// increment degree counter
		}


		fireMesh.vertices = newPoints;
		newPoints [20] = Vector3.zero;
		fireMesh.name = "fire";
		fireBreathHitbox.SetActive (true);
		int i = 0;
		for (int x = 0; x < 19; x++) {
			Debug.Log (x);
			triangles [i] = x;
			triangles [i+1] = x+1;
			triangles [i+2] = 20;
			i += 3;

		}

		fireMesh.triangles = triangles;
		FireHitbox.points = newPointsv2;
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
			StartCoroutine (FireBreath ());
		 
		} else if (Vector3.Distance (AS.GetTargetPosition (), transform.position) < 2) {
			int rand = Random.Range (0, 100);
			if (rand <= 75) {
				StartCoroutine (SlashAttack ());
			} else {
				StartCoroutine (TailWhip ());
			}

		} else {
			Invoke ("ExecuteAttack", .2f);
		}
	}
}
