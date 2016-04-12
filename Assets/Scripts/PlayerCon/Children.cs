using UnityEngine;
using System.Collections;

public class Children : MonoBehaviour{
	
	public GameObject childrenObj;
	void Start () {
		childrenObj.transform.parent = gameObject.transform;
	}

}
