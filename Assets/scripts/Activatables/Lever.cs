using UnityEngine;
using System.Collections;

public class Lever : Activatable
{
	public float coolDown = 0.2f;
	float time;

	public override void activate (CharCtrl player)
	{
		if (time > 0f)
			return;
		if (activated) {
			sr.sprite = deactivatedTex;
			activated = false;
			onDeactivation (player);
		} else {
			sr.sprite = activatedTex;
			activated = true;
			onActivation (player);
			if (rg != null)
				rg.checkout (this, player);
		}
		if (chainedActivatable != null)
			chainedActivatable.activate (player);
		time = coolDown;
	}

	public override void onActivation (CharCtrl player)
	{
		//SoundManager.script.playOnListener (SoundManager.script.click0, 0.2f);
	}

	public override void onDeactivation (CharCtrl player)
	{
		//SoundManager.script.playOnListener (SoundManager.script.click0, 0.2f);
	}

	void Update ()
	{
		time -= Time.deltaTime;
	}
}
