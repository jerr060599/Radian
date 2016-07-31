using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour
{
    public Texture2D mouse;
    public static CustomCursor script;
    public GameObject smaller, cam, wpIndicator;
    public float smallerSpacing = 0.5f, waypointBorderSpacing = 1f;
    public Waypoint curWaypoint = null;
    RectTransform smallerRt, wpRT;
    void Awake()
    {
        script = this;
        smallerRt = smaller.GetComponent<RectTransform>();
        wpRT = wpIndicator.GetComponent<RectTransform>();
        Cursor.SetCursor(mouse, new Vector2(16f, 16f), CursorMode.Auto);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CharCtrl.script.transform.position;
        smallerRt.localPosition = (Vector2)(CharCtrl.script.transform.position) + Vector2.ClampMagnitude(dPos * smallerSpacing, CharCtrl.script.dashDist);
        if (curWaypoint)
        {
            if (!wpIndicator.activeSelf)
                wpIndicator.SetActive(true);
            Vector2 dWpPos = curWaypoint.gameObject.transform.position - CameraMovement.script.transform.position;
            Vector2 screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - CameraMovement.script.transform.position;
            screenBound.x -= waypointBorderSpacing;
            screenBound.y -= waypointBorderSpacing;
            if (Mathf.Abs(dWpPos.x) < screenBound.x && Mathf.Abs(dWpPos.y) < screenBound.y)
                wpRT.localPosition = curWaypoint.transform.position;
            else if (dWpPos.x == 0)
                wpRT.localPosition = new Vector3(CameraMovement.script.transform.position.x,
                    CameraMovement.script.transform.position.y + (dWpPos.y > 0 ? screenBound.y : -screenBound.y), 0f);
            else if (Mathf.Abs(dWpPos.y / dWpPos.x) > screenBound.y / screenBound.x)
            {
                float dy = (dWpPos.y > 0 ? -screenBound.y : screenBound.y) + dWpPos.y;
                wpRT.localPosition = new Vector3(CameraMovement.script.transform.position.x + dWpPos.x - dy / dWpPos.y * dWpPos.x,
                    CameraMovement.script.transform.position.y + (dWpPos.y > 0 ? screenBound.y : -screenBound.y), 0f);
            }
            else if (dWpPos.x != 0)
            {
                float dx = (dWpPos.x > 0 ? -screenBound.x : screenBound.x) + dWpPos.x;
                wpRT.localPosition = new Vector3(CameraMovement.script.transform.position.x + (dWpPos.x > 0 ? screenBound.x : -screenBound.x),
                    CameraMovement.script.transform.position.y + dWpPos.y - dx * dWpPos.y / dWpPos.x, 0f);
            }
            else
                wpRT.localPosition = new Vector3(CameraMovement.script.transform.position.x + (dWpPos.x > 0 ? screenBound.x : -screenBound.x),
                    CameraMovement.script.transform.position.y, 0f);
        }
        else if (wpIndicator.activeSelf)
            wpIndicator.SetActive(false);
    }
}
