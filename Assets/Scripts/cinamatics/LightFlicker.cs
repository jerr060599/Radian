using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public float minIntensity = 1.5f;
	public float maxIntensity = 3f;

	float random;

	void Start()
	{
		random = Random.Range(0.0f, 65535.0f);
	}

	void Update()
	{
		float noise = Mathf.PerlinNoise(random, Time.time);
	
		GetComponent<Light>().range = Mathf.Lerp(5, 20, noise);
		int enabled = Random.Range (0, 80);
		if (enabled == 2)
			GetComponent<Light> ().intensity =0.3f;
		else
			GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}
