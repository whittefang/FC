using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {
	public GameObject exit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && exit.activeSelf) {
			other.transform.position = exit.transform.transform.position;
		}
	}
}
