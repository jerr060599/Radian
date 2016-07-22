using UnityEngine;
using System.Collections;

public class OnImpact : MonoBehaviour
{
	public float impactThreshold = 10f;

	void OnCollisionEnter2D (Collision2D c)
	{
		if (-c.relativeVelocity.y > impactThreshold)
			impact ();
	}

	public virtual void impact ()
	{
	}
}
