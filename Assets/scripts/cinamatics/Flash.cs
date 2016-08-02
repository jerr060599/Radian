using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{
    public float fade = 0.9f;
    Color c;
    UnityEngine.UI.Image img;
    void Start()
    {
        img = GetComponent<UnityEngine.UI.Image>();
        c = img.color;
    }
    void Update()
    {
        AudioListener.volume += (1 - AudioListener.volume) * fade;
        c.a *= fade;
        img.color = c;
        if (c.a < 0.05f && AudioListener.volume > 0.95f)
        {
            AudioListener.volume = 1f;
            c.a = 0f;
            Destroy(gameObject);
        }
    }
}
