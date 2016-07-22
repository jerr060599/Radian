using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutoSnap : MonoBehaviour
{
    public int xOffset = 0, yOffset = 0;
    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            float a = transform.localRotation.eulerAngles.z / 180f * Mathf.PI;
            float ux = Mathf.Cos(a);
            float uy = Mathf.Sin(a);
            float xp = Mathf.Floor((ux * transform.localPosition.x + uy * transform.localPosition.y + 1f / 80f) * 40f) / 40 + xOffset;
            float yp = Mathf.Floor((-uy * transform.localPosition.x + ux * transform.localPosition.y + 1f / 80f) * 40f) / 40 + yOffset;
            transform.localPosition = new Vector3(ux * xp - uy * yp, uy * xp + ux * yp, transform.localPosition.z);
        }
    }
}
