using UnityEngine;
using System.Collections;

public class WarriorAttackScript : MonoBehaviour {
	public GameObject sunderHitbox, hookHitbox, shield, reticle;
	public HookScript HS;
	public ProjectileScript hookProjectileScript;
	InputScript IS;
	PlayerMovementScript playerMovement;
	public HitboxScript swordHitboxScript;
	bool isCasting = false;
	int spellNumber, hookCost = 50, chargeCost = 50, shieldCost = 25;
	public int rage = 100;
	float castingCounter, reticleSpeed = .2f;
	// Use this for initialization
	void Start () {
		swordHitboxScript.SetOptFunc (GenerateRage);
		playerMovement = GetComponent<PlayerMovementScript> ();
		IS = GetComponent<InputScript> ();
		IS.assignXButton (Sunder, null );
		IS.assignAButton (ChargePress , ChargeRelease);
		IS.assignBButton (HookCast, HookThrow );
		IS.assignYButton (ShieldUp, ShieldRelease );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Sunder(){
		if (startCasting(0)) {
			StartCoroutine (SunderNumer ());
		}
	}
	IEnumerator SunderNumer(){
		int angle = 0;
		sunderHitbox.SetActive (true);
		for (int x = 0; x < 10; x++) {
			
			sunderHitbox.transform.eulerAngles = new Vector3 (0, 0, angle);
			angle += 18;
			yield return null;
		}
		sunderHitbox.SetActive (false);
		yield return new WaitForSeconds (.3f);
		stopCasting (0);

	}
	void HookCast(){
		if (rage >= hookCost) {
			if (startCasting (1)) {
				StartCoroutine (CastingReticle ());
			}
		}
	}
	void HookThrow(){
		if (CheckCasting(1)) {
			StartCoroutine (HookNumer());

		}
	}
	IEnumerator HookNumer(){
		rage -= hookCost;
		reticle.SetActive (false);
		hookHitbox.SetActive (true);
		HS.EnableHook ();
		hookHitbox.transform.position = transform.position;
		hookProjectileScript.SetDirection ( (Vector2)Vector3.Normalize( reticle.transform.position - transform.position));
		float x = Time.time;
		while (Time.time - x < .35f) {
			if (HS.objectHooked) {
				x = Time.time - x;
				break;
			}
			yield return null;
		}
		if (x > .35f) {
			x = .35f;
		}
		x -= .05f;
		hookProjectileScript.SetDirection ((Vector2)Vector3.Normalize (transform.position - hookHitbox.transform.position ));
		yield return new WaitForSeconds (x);
		hookHitbox.SetActive (false);
		HS.ResetMovement ();
		stopCasting (1);
	}
	void ShieldUp(){
		if (rage >= shieldCost){
			if (startCasting(2) ){
				shield.SetActive (true);
			}
		}
	}
	void ShieldRelease(){
		if (stopCasting (2)) {
			rage -= shieldCost;
			shield.SetActive (false);
		}

	}
	void ChargePress(){
		if (rage >= chargeCost){
			startCasting (3);
		}
	}
	void ChargeRelease(){
		if (stopCasting (3)) {
			rage -= chargeCost;
			playerMovement.Charge (35, new Vector2 (IS.GetX (), IS.GetY ()), .15f); 
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
			spellNumber = -1;
			return true;
		}else {
			return false;
		}
	}
	bool CheckCasting(int spell){
		return spellNumber == spell;
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

	public void GenerateRage(){
		rage += 5;
		if (rage > 100) {
			rage = 100;
		}
	}

}
