using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour
{
    public Texture2D mouse;
    public static CustomCursor script;
    public GameObject small, smaller;
    public float smallSpacing = 0.5f, smallerSpacing = 0.7f;
    RectTransform smallRt, smallerRt;
    void Start()
    {
        script = this;
        smallRt = small.GetComponent<RectTransform>();
        smallerRt = smaller.GetComponent<RectTransform>();
        Cursor.SetCursor(mouse, new Vector2(13f,13f), CursorMode.Auto);
    }
    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused)
            return;
        Vector3 half = new Vector3(Screen.width / 2, Screen.height / 2);
        Vector3 cPos = Camera.main.WorldToScreenPoint(CharCtrl.script.center) - half;
        Vector3 dPos = (Vector3)cPos - Input.mousePosition + half;
        smallRt.localPosition = cPos - dPos * smallSpacing;
        smallerRt.localPosition = cPos - dPos * smallerSpacing;
    }
}
