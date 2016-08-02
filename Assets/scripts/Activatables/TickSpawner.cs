using UnityEngine;
using System.Collections;

public class TickSpawner : Activatable
{
	public GameObject prefab;
	public float spawnTime;
	float time;
	// Use this for initialization
	public override void init ()
	{
		time = activated ? spawnTime : 1000000000f;
	}

	public override void activate (CharCtrl pl)
	{
		activated = !activated;
		time = activated ? spawnTime : 1000000000f;
		if (nextActivatable != null)
			nextActivatable.activate (pl);
	}

	void Update ()
	{
		time -= Time.deltaTime;
		if (time <= 0f) {
			Instantiate (prefab, transform.position, transform.rotation);
			time = spawnTime;
		}
	}
}
