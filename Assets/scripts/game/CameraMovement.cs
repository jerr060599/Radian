using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement script = null;
    public CameraOverride camOverride = null;
    GameObject c0;
    public float sizeTar = 140f, smooth = 0.06f, maxMouseOffset = 3f, mouseSensitivity = 0.005f;
    Vector3 shakePos = Vector3.zero, mousePos = Vector3.zero, curPos;
    Camera cam;
    void Awake()
    {
        script = this;
        curPos = transform.position;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        mousePos += (Vector3.ClampMagnitude(new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0f) * mouseSensitivity, maxMouseOffset) - mousePos) * smooth;
        if (camOverride)
        {
            camOverride.duration -= Time.deltaTime;
            cam.transform.position = (curPos += ((Vector3)(camOverride.pos) - curPos) * smooth) + shakePos + mousePos;
            if (camOverride.duration <= 0f)
            {
                Destroy(camOverride.gameObject);
                CharCtrl.script.controllable = true;
                camOverride = null;
            }
        }
        else
            cam.transform.position = (curPos += (CharCtrl.script.gameObject.transform.position - curPos) * smooth) + shakePos + mousePos;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);
        shakePos *= 0.7f;
        cam.orthographicSize += (sizeTar - (float)cam.orthographicSize) * smooth;
    }

    public void shake(float amplitude = 2f)
    {
        shakePos = new Vector3(Random.value, Random.value, 0f).normalized * amplitude;
    }
}
