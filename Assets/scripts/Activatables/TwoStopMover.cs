using UnityEngine;
using System.Collections;

public class TwoStopMover : Activatable
{
	public float x2, y2;
	public float velocity;
	public float pause;
	float x1, y1, stopTime, dx, dy;
	bool toPointOne = false;
	Rigidbody2D pysc;
	// Use this for initialization
	public override void init ()
	{
		x1 = transform.position.x;
		y1 = transform.position.y;
		dx = x2 - x1;
		dy = y2 - y1;
		pysc = GetComponent<Rigidbody2D> ();
		stopTime = pause;
	}

	public override void activate (CharCtrl c)
	{
		stopTime = 0f;
		if (chainedActivatable != null)
			chainedActivatable.activate (c);
	}
	// Update is called once per frame
	void Update ()
	{
		stopTime -= Time.deltaTime;
		if (stopTime <= 0f) {
			Vector3 dir;
			if (toPointOne)
				dir = new Vector3 (x1 - transform.position.x, y1 - transform.position.y, 0f);
			else
				dir = new Vector3 (x2 - transform.position.x, y2 - transform.position.y, 0f);
			if (pysc.velocity.x == 0 && pysc.velocity.y == 0)
				pysc.velocity = dir.normalized * velocity;
			if ((toPointOne ? -1f : 1f) * (dir.x * dx + dir.y * dy) <= 0f) {
				pysc.velocity = Vector3.zero;
				pysc.position = toPointOne ? new Vector2 (x1, y1) : new Vector2 (x2, y2);
				stopTime = pause;
				toPointOne = !toPointOne;
			}
		} else
			pysc.velocity = Vector3.zero;
	}
}
