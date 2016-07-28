using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	float minIntensity = 1.5f;
	float maxIntensity = 3f;

	float random;

	void Start()
	{
		random = Random.Range(0.0f, 65535.0f);
	}

	void Update()
	{
		float noise = Mathf.PerlinNoise(random, Time.time);
		GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}
