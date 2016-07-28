using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Test : MonoBehaviour {
    public bool activate = false;
	// Update is called once per frame
	void Update () {
        if (activate)
        {
            Debug.Log(GetComponent<SpriteRenderer>().bounds.min);
            activate = false;
        }
	}
}
