using UnityEngine;
using System.Collections;

public class FireballAfterBurnScript : MonoBehaviour {
	public GameObject trigger;
	ProjectileScript PS;
	HitboxScript parentHitbox;
	BoxCollider2D parentTrigger;
	SpriteRenderer parentSprite;
	// Use this for initialization
	void Awake () {
		parentTrigger = GetComponent<BoxCollider2D> ();
		parentSprite = GetComponent<SpriteRenderer> ();
		parentHitbox = GetComponent<HitboxScript> ();
		PS = GetComponent<ProjectileScript> ();
		GetComponent<HitboxScript> ().SetOptFunc (Hit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Hit(){
		PS.MovementEnabled = false;
		StartCoroutine (AfterburnTimer ());
	}
	IEnumerator AfterburnTimer(){
		parentHitbox.EnableScript(false);
		parentSprite.enabled = false;
		parentTrigger.enabled = false;
		for (int x = 0; x < 30; x++) {
			trigger.SetActive (true);
			yield return new WaitForSeconds (.1f);
			trigger.SetActive (false);
		}
		gameObject.SetActive (false);
	}
}
