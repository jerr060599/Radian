using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TriggerScene : MonoBehaviour {

	public string scene;
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) {
			SceneManager.LoadScene (scene);
		}
	}
}
