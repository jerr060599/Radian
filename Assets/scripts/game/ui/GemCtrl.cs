using UnityEngine;
using System.Collections;

public class GemCtrl : MonoBehaviour
{
    public bool isLight = true;
    public UnityEngine.UI.Image sr = null;
    public Color light, dark;
    public float smooth = 0.1f;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLight)
            sr.color = new Color(sr.color.r - (sr.color.r - light.r) * smooth, sr.color.g - (sr.color.g - light.g) * smooth, sr.color.b - (sr.color.b - light.b) * smooth);
        else
            sr.color = new Color(sr.color.r - (sr.color.r - dark.r) * smooth, sr.color.g - (sr.color.g - dark.g) * smooth, sr.color.b - (sr.color.b - dark.b) * smooth);
    }
}
