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
        c.a *= fade;
        img.color = c;
        if (c.a < 0.05f)
            Destroy(gameObject);
    }
}
