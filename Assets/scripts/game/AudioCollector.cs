using UnityEngine;
using System.Collections;

public class AudioCollector : MonoBehaviour
{
	AudioSource src;

	void Start ()
	{
		src = GetComponent<AudioSource> ();
	}

	void LateUpdate ()
	{
		if (!src.isPlaying)
			Destroy (gameObject);
	}
}
