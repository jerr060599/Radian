using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement script = null;
    GameObject c0;
    public float sizeTar = 140f, smooth = 0.06f, maxMouseOffset = 3f, mouseSensitivity = 0.005f;
    Vector3 shakePos = Vector3.zero, mousePos = Vector3.zero, curPos;
    Camera cam;

    void Start()
    {
        script = this;
        curPos = transform.position;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        mousePos += (Vector3.ClampMagnitude(new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0f) * mouseSensitivity, maxMouseOffset) - mousePos) * smooth;
        cam.transform.position = (curPos += (CharCtrl.script.gameObject.transform.position - curPos) * smooth) + shakePos + mousePos;
        shakePos *= 0.7f;
        cam.orthographicSize += (sizeTar - (float)cam.orthographicSize) * smooth;
    }

    public void shake(float amplitude = 2f)
    {
        shakePos = new Vector3(Random.value, Random.value, 0f).normalized * amplitude;
    }
}
