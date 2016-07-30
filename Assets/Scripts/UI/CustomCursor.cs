using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour
{
    public Texture2D mouse;
    public static CustomCursor script;
    public GameObject smaller, cam;
    public float smallerSpacing = 0.5f;
    RectTransform smallerRt;
    void Start()
    {
        script = this;
        smallerRt = smaller.GetComponent<RectTransform>();
        Cursor.SetCursor(mouse, new Vector2(13f, 13f), CursorMode.Auto);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CharCtrl.script.transform.position;
        smallerRt.localPosition = (Vector2)(CharCtrl.script.transform.position) + Vector2.ClampMagnitude(dPos * smallerSpacing, CharCtrl.script.dashDist);
    }
}
