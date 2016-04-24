using UnityEngine;
using System.Collections;

public class AggroScript : MonoBehaviour {
	int[] aggroListAmount;
	public Transform[] aggroListPlayer;
	public int currentTarget = -1;
	public delegate void genFunc ();
	public AIMovementScript AMS;
	// Use this for initialization
	void Awake () {
		aggroListAmount = new int[4];
		aggroListPlayer = new Transform[4];
		AMS = GetComponent<AIMovementScript> ();
	}
	void OnEnable(){
		aggroListAmount = new int[4];
		for (int x = 0; x < 4; x++) {
			aggroListAmount [0] = 0;
		}
		GetPlayers();
		StartCoroutine (CheckForTargets ());
			
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void IncreaseAggro(int pNum, int amount){
		aggroListAmount [pNum] += amount;
	}
	void CalculateAggro(){
		if (currentTarget >= 0) {
			for (int x = 0; x < 4; x++) {
				if (aggroListAmount [x] > aggroListAmount [currentTarget]) {
					currentTarget = x;
					Debug.Log ("change");
					if (AMS != null) {
						AMS.SetTarget (aggroListPlayer [currentTarget]);
					}
				}
			}
		}
	}
	public Vector3 GetTargetPosition(){
		if (currentTarget >= 0) {
			return aggroListPlayer [currentTarget].position;		
		}else {
			return new Vector3 (-100, -100, -100);
		}
	}

	IEnumerator CheckForTargets(){
		while (currentTarget == -1) {
			int mask = 1 << 8;
			RaycastHit2D players = Physics2D.CircleCast (transform.position, 5f, Vector2.zero, 0, mask);
			if (players.collider != null) {
				Debug.Log (players.collider.gameObject.name);
				for (int i = 0; i < 4; i++) {
					if (aggroListPlayer [i].gameObject && (aggroListPlayer [i].gameObject == players.collider.gameObject)) {
						IncreaseAggro (i, 100);
						currentTarget = i;
						if (AMS != null) {
							AMS.SetTarget (aggroListPlayer [currentTarget]);
						}
					}
				} 
			}
			yield return new WaitForSeconds (.5f);
		}
	}
	void GetPlayers(){
		aggroListPlayer[0] =  GameObject.Find ("Warrior").transform;
		aggroListPlayer[1] =  GameObject.Find ("Priest").transform;
		aggroListPlayer[2] =  GameObject.Find ("Mage").transform;
		aggroListPlayer[3] =  GameObject.Find ("Rogue").transform;
	}
}
