using UnityEngine;
using System.Collections;

public class MageAttacksScript : MonoBehaviour {


	InputScript IS;
	PlayerMovementScript playerMovement;
	public GameObject reticle, blizzardHitbox, portalEntrance, portalExit, vortextHitbox;
	public ObjectPoolScript pool;
	bool isCasting = false, firstPortal = true;
	float castingCounter = 0, reticleSpeed = .1f;
	int spellNumber = 0;
	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovementScript> ();
		IS = GetComponent<InputScript> ();
		IS.assignXButton (FireBallCast, FireBallAttack );
		IS.assignAButton (PortalCast , PortalFinish);
		IS.assignBButton (BlizzardCast, BlizzardAttack );
		IS.assignYButton (VortexCast, VortexAttack );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// press button to start casting and aim firebal
	void FireBallCast(){
		startCasting (0);
	}
	void FireBallAttack(){
		if (  stopCasting(0) && ( Time.time - castingCounter) > .2f){
			float fireAngle =  FCMath.LockDirection (IS.GetX(), IS.GetY());
			GameObject tmp =  pool.FetchObject ();
			tmp.transform.eulerAngles = new Vector3 (0, 0, fireAngle);
			tmp.transform.position = transform.position;
			Vector2 direction = FCMath.LockDirectionV2 (IS.GetX (), IS.GetY ());
			if (direction == Vector2.zero) {
				direction = Vector2.one;
			}
			tmp.GetComponent<ProjectileScript>().SetDirection(direction);
			tmp.SetActive (true);
		}
	}
	void BlizzardCast(){
		if (startCasting (1)) {
			StartCoroutine (CastingReticle ());
		}
	}
	void BlizzardAttack(){
		if ( stopCasting (1) && (Time.time - castingCounter) > .5f ) {
			blizzardHitbox.transform.position = reticle.transform.position;
			blizzardHitbox.SetActive (true);
		}

	}
	void PortalCast(){
		startCasting (2);


	}
	void PortalFinish(){
		if ( stopCasting (2) && (Time.time - castingCounter) > .1f) {
			if (firstPortal && !portalEntrance.activeSelf) {
				portalEntrance.transform.position = transform.position;
				portalEntrance.SetActive (true);
				firstPortal = !firstPortal;
			} else if (!firstPortal && !portalExit.activeSelf){
				portalExit.transform.position = transform.position;
				portalExit.SetActive (true);
				firstPortal = !firstPortal;
			}


		}

	
	}
	void VortexCast(){
		if (startCasting (3)) {
			StartCoroutine (CastingReticle ());
		}
	
	}
	void VortexAttack(){
		if (stopCasting (3) && (Time.time - castingCounter) > .3f ) {
			vortextHitbox.transform.position = reticle.transform.position;
			vortextHitbox.SetActive (true);
		}
	
	}

	bool startCasting(int spell){
		if (!isCasting){
			spellNumber = spell;
			isCasting = true;
			castingCounter = Time.time;
			playerMovement.SetSpeed (0);
			return true;
		}else {
			return false;
		}

	}
	bool stopCasting(int spell){
		if (spellNumber == spell) {
			playerMovement.ResetSpeed ();
			isCasting = false;
			return true;
		}else {
			return false;
		}
	}
	IEnumerator CastingReticle(){
		reticle.SetActive (true);
		reticle.transform.position = transform.position;
		while (isCasting) {
			reticle.transform.position += new Vector3 (IS.GetX (false) * reticleSpeed, IS.GetY (false) * reticleSpeed, 0);
			yield return null;
		}
		reticle.SetActive (false);
	}
}
