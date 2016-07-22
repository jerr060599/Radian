using UnityEngine;
using System.Collections;

public class SoundOnImpact : OnImpact
{
	public AudioClip sound;
	public float volume;

	public override void impact ()
	{
		SoundManager.script.playOnListener (sound, volume);
	}
}
