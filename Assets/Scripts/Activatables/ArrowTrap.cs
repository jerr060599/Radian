using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {

	public GameObject arrowSpawn;
	public GameObject arrow;
	float a,t;
	public float velocity;
	void Start()
	{
		t = 1f;
		a = t;

	}
	void OnTriggerStay2D(Collider2D col)
	{
		a -= Time.deltaTime;
		if (a < 0.01f) {
			GameObject arrowInstance=Instantiate (arrow, arrowSpawn.transform.position, arrowSpawn.transform.rotation) as GameObject;
			arrowInstance.GetComponent<Projectile> ().setVelocity (Vector2.down*velocity);
		
			a = t;
		}
		GetComponent<Animator>().Play("fire");
	}

	void OnTriggerExit2D(Collider2D col)
	{

		GetComponent<Animator>().Play("neutral");
	}
}
