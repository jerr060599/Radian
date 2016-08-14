using UnityEngine;
using System.Collections;

public class SoundOnTrigger : MonoBehaviour {

	public AudioClip sound;
	public bool once;
	bool playAgain;

	void Start()
	{
		playAgain = once;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player" && once==playAgain) {
			SoundManager.script.playOn(transform, sound);
			if(once)
				once=false;
		}

	}
}
