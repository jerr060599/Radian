using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Test : MonoBehaviour {
    public bool activate = false;
	// Update is called once per frame
	void Update () {
        if (activate)
        {
            GameObject txt = new GameObject();
            txt.AddComponent<CanvasRenderer>();
            txt.AddComponent<RectTransform>();
            txt.AddComponent<UnityEngine.UI.Text>();
            activate = false;
        }
	}
}
