using UnityEngine;
using System.Collections;

public class BarCtrl : MonoBehaviour
{
    public float barPercent = 1f, offset = 100f, smooth = 0.1f;
    public GameObject overlay;
    float curPercent = 1f;

    void Update()
    {
        curPercent += (barPercent - curPercent) * smooth;
        transform.localPosition = new Vector3((1 - curPercent) * offset, transform.localPosition.y, 0f);
        if (overlay)
            overlay.transform.localPosition = transform.localPosition;
    }
}
