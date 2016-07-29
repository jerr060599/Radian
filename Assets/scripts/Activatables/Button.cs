using UnityEngine;
using System.Collections;

public class Button : Activatable
{
	public float coolDown = 1f;
	public AudioClip sound;
	float time;

	public override void init ()
	{
	}

	public override void activate (CharCtrl player)
	{
		if (time <= 0f) {
			sr.sprite = deactivatedTex;
			SoundManager.script.playOnListener (sound, 0.7f);
			time = coolDown;
			if (nextActivatable != null)
				nextActivatable.activate (player);
		}
	}

	void Update ()
	{
		time -= Time.deltaTime;
		if (time <= 0f && sr.sprite != activatedTex)
			sr.sprite = activatedTex;
	}
}
