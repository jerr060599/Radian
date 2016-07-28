using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AutoOrder : MonoBehaviour
{
    public float offset = 0f, shadowScale = 2f;
    public bool isStatic = false, orderOnY = true, generateShadow = false;
    public GameObject shadow = null;
    SpriteRenderer sr = null;
    void Update()
    {
        if (isStatic && !(Application.isEditor && !Application.isPlaying))
            Destroy(this);
        if (!sr)
            sr = GetComponent<SpriteRenderer>();
        if (orderOnY)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (transform.localPosition.y + offset) / 100f);
        if (generateShadow && !shadow)
        {
            shadow = new GameObject();
            shadow.name = "shadow";
            shadow.transform.SetParent(gameObject.transform);
            shadow.AddComponent<SpriteRenderer>();
            SpriteRenderer shadowSr = shadow.GetComponent<SpriteRenderer>();
            shadowSr.sprite = sr.sprite;
            shadowSr.color = new Color(0f, 0f, 0f, 0.5f);
            shadow.transform.localScale = new Vector3(1f, shadowScale, 1f);
            float dMin = sr.bounds.extents.y - shadowSr.bounds.extents.y;
            shadow.transform.localPosition = new Vector3(0f, -dMin, 0f);
        }
        if (!generateShadow && shadow)
        {
            DestroyImmediate(shadow);
            shadow = null;
        }
    }
}
