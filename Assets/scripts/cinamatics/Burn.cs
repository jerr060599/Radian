using UnityEngine;
using System.Collections;

public class Burn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().Play ("burning");
	}

}
