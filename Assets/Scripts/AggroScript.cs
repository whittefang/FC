using UnityEngine;
using System.Collections;

public class AggroScript : MonoBehaviour {
	int[] aggroListAmount;
	Transform[] aggroListPlayer;
	int currentTarget = -1;
	// Use this for initialization
	void Awake () {
		aggroListAmount = new int[4];
		aggroListPlayer = new Transform[4];
	}
	void OnEnable(){
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
		for (int x = 0; x < 4; x++) {
			if (aggroListAmount [x] > aggroListAmount [currentTarget]) {
				currentTarget = x;
			}
		}
	}
	public Transform GetTargetTransform(){
		return aggroListPlayer[currentTarget];		
	}

	IEnumerator CheckForTargets(){
		while (currentTarget == -1) {
			int mask = 1 << 7;
			RaycastHit2D players =  Physics2D.CircleCast (transform.position, 4f,Vector2.zero, 0 , mask);
				for (int i = 0; i < 4; i++) {
					if (aggroListPlayer [i].gameObject == players.collider.gameObject) {
						currentTarget = i;
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
