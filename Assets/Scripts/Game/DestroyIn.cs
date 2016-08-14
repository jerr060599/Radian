using UnityEngine;
using System.Collections;

public class DestroyIn : MonoBehaviour {

	public float t;


	void Update () {
		t -= Time.deltaTime;
		if (t < 0.01f)
			Destroy (gameObject);
	}
}
