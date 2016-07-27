using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TriggerScene : MonoBehaviour {

	public string scene;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			SceneManager.LoadScene (scene);
		}
	}
}
