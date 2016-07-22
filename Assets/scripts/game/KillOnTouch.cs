using UnityEngine;
using System.Collections;

public class KillOnTouch : MonoBehaviour
{
	public Sprite bloodied = null;
	public AudioClip sound = null;
	public float volume = 1f;
	// Use this for initialization
	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.collider.attachedRigidbody == null)
			return;
		CharCtrl cc = c.collider.attachedRigidbody.gameObject.GetComponent<CharCtrl> ();
		if (cc != null) {
			cc.kill ();
			if (bloodied != null)
				GetComponent<SpriteRenderer> ().sprite = bloodied;
			if (sound != null)
				SoundManager.script.playOnListener (sound, volume);
		}
	}
}
