using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{
    public float fade = 0.9f;
	float a,t;
    Color c;
    UnityEngine.UI.Image img;
    void Start()
    {
        img = GetComponent<UnityEngine.UI.Image>();
        c = img.color;
		t = 0.08f;
		a = t;
    }
    void Update()
    {
		
        AudioListener.volume += (1 - AudioListener.volume) * fade;
		a -= Time.deltaTime;
		if (a < 0.01f) {
			c.a *= fade;
		
			img.color = c;
			a = t;
		}
        if (c.a < 0.05f && AudioListener.volume > 0.95f)
        {
            AudioListener.volume = 1f;
            c.a = 0f;
            Destroy(gameObject);
        }
    }
}
