using UnityEngine;
using System.Collections;

public class ActivateIn : MonoBehaviour {

	public float t;
	public GameObject obj;

	void Update () {
		t -= Time.deltaTime;
		if (t < 0.01f)
			obj.SetActive (true);
	}
}
