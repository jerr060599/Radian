using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour {

	public GameObject subtitle,canvas;
	GameObject spawnedText;
	GameObject SubSpawn;
	public string[] content;
	Text sText;
	bool showText=false;
	public float t;
	float a;
	GameObject Text;
	int index;

	public bool once;
	bool show=true;

	void Start () {
		SubSpawn = GameObject.Find ("SubSpawn") as GameObject;

		spawnedText = Instantiate (subtitle,SubSpawn.transform.position,SubSpawn.transform.rotation) as GameObject;
		spawnedText.transform.SetParent(canvas.transform);
		sText = spawnedText.GetComponent<Text> ();
		sText.enabled = false;
		a = t;
		index = content.Length;
	}

	void Update () {


		if (index != content.Length) {
			
			sText.text = content [index];
			sText.enabled = true;

			a -= Time.deltaTime;

			if (a < 0.01f) {
				index++;
				a = t;
			}

		} else {

			sText.enabled = false;

		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (show) {
				index = 0;
				if(once)
				show = false;
			}
		}

	}
}
