using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ExitGame : MonoBehaviour {


	public void OnClick()
	{
		GetComponent<AudioSource> ().Play ();
		Application.Quit ();

	}
}
