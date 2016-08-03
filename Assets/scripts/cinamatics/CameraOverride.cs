using UnityEngine;
using System.Collections;

public class CameraOverride : MonoBehaviour
{
    public Vector2 pos;
    public float size, duration = 1f;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject && duration > 0f)
        {
            CameraMovement.script.camOverride = this;
            CharCtrl.script.controllable = false;
        }
    }
}
