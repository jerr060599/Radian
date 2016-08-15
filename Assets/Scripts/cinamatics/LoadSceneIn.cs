using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadSceneIn : MonoBehaviour {

	public float t;
	public string scene;
	void Update () {
		t -= Time.deltaTime;
		if (t < 0.01f)
			SceneManager.LoadScene (scene);
	}
}
