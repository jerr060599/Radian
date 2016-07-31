using UnityEngine;
using System.Collections;

public class AutoArrow : MonoBehaviour {
	public GameObject arrowSpawn;
	public GameObject arrow;
	public float a,t;
	public float velocity;
	void Start()
	{
		t = 1f;
		a = t;

	}
	void Update()
	{
		a -= Time.deltaTime;
		if (a < 0.01f) {
			GameObject arrowInstance=Instantiate (arrow, arrowSpawn.transform.position, arrowSpawn.transform.rotation) as GameObject;
			arrowInstance.GetComponent<Projectile> ().setVelocity (Vector2.down*velocity);


			a = t;
		}
		GetComponent<Animator>().Play("fire");
	}

}
