using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {

	public GameObject arrowSpawn;
	public GameObject arrow;
	float a,t;
	void Start()
	{
		t = 0.5f;
		a = t;

	}
	void OnTriggerStay()
	{
		a -= Time.deltaTime;
		if (a < 0.01f) {
			Instantiate (arrow, arrowSpawn.transform.position, arrowSpawn.transform.rotation);
			Debug.Log ("Shot");
			a = t;
		}
		GetComponent<Animator>().Play("fire");
	}

	void OnTriggerExit()
	{

		GetComponent<Animator>().Play("neutral");
	}
}
