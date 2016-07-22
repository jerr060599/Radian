using UnityEngine;
using System.Collections;

public abstract class Consumable : MonoBehaviour
{
	public Sprite icon;
	public float respawnTime;
	float time;
	SpriteRenderer sr;
	Collider2D col;

	public void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
		col = GetComponent<Collider2D> ();
	}

	void OnTriggerEnter2D (Collider2D c)
	{
        CharCtrl cc;
		if ((cc = c.attachedRigidbody.gameObject.GetComponent<CharCtrl> ()) != null) {
			if (cc.eat (this)) {
				sr.enabled = false;
				col.enabled = false;
				time = 1000000f;
			}
		}
	}

	void Update ()
	{
		time -= Time.deltaTime;
		if (!sr.enabled && time <= 0) {
			sr.enabled = true;
			col.enabled = true;
		}
	}

	public void use (CharCtrl player)
	{
		onConsumption (player);
		time = respawnTime;
	}

	public abstract void onConsumption (CharCtrl player);
}
