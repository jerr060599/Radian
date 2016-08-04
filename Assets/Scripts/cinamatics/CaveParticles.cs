using UnityEngine;
using System.Collections;

public class CaveParticles : MonoBehaviour {

	public float emissionTimer;

	void Update()
	{
		emissionTimer -= Time.deltaTime;
		if(emissionTimer<0.01f)
		{
			GetComponent<ParticleSystem> ().emissionRate = 0;
		}


	}
}
