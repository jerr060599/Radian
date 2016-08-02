using UnityEngine;
using System.Collections;

public class PushSpawner : Activatable
{
	public float coolDown;
	public int numOfUse;
	public GameObject prefab;
	float time;

	public override void init ()
	{
	}

	public void setUse (int use)
	{
		numOfUse = Mathf.Max (use, 0);
		sr.sprite = numOfUse == 0 ? deactivatedTex : activatedTex;
	}
	// Use this for initialization
	public override void activate (CharCtrl pl)
	{
		if (time <= 0f && numOfUse > 0) {
			Instantiate (prefab, transform.position, transform.rotation);
			time = coolDown;
			if (--numOfUse <= 0)
				sr.sprite = deactivatedTex;
		}
		if (nextActivatable != null)
			nextActivatable.activate (pl);
	}
	
	// Update is called once per frame
	void Update ()
	{
		time -= Time.deltaTime;
	}
}
