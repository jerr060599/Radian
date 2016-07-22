using UnityEngine;
using System.Collections;

public class ConstantRoll : MonoBehaviour
{
	public float w;
	public Rigidbody2D pysc;
	// Use this for initialization
	void Start ()
	{
		pysc = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.collider.attachedRigidbody == null)
			return;
		ConstantRoll cr;
		if ((cr = c.collider.attachedRigidbody.GetComponent<ConstantRoll> ()) != null) {
			if (w * cr.w < 0f && gameObject.transform.position.x - cr.gameObject.transform.position.x > 0f) {
				w *= -1f;
				cr.w *= -1f;
				CameraMovement.script.shake ();
				SoundOnImpact si;
				if ((si = gameObject.GetComponent<SoundOnImpact> ()) != null)
					si.impact ();
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		pysc.angularVelocity = w;
	}
}
