using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AutoOrder : MonoBehaviour
{
    public float offset = 0f, shadowScale = 1.5f, shadowOffset = 0f, shadowDarkness = 0.3f;
    public bool isStatic = false, orderOnY = true, generateShadow = false, updateShadowInRuntime = false, refresh = false;
    public GameObject shadow = null;
    SpriteRenderer sr = null;
    void Update()
    {
        if (!sr)
            sr = GetComponent<SpriteRenderer>();
        if (!Application.isEditor || Application.isPlaying)
        {
            if (isStatic)
                Destroy(this);
            if (updateShadowInRuntime && shadow)
            {
                if (shadow.GetComponent<SpriteRenderer>().sprite != sr.sprite)
                    genShadow();
                shadow.GetComponent<SpriteRenderer>().flipX = sr.flipX;
            }
        }
        if (refresh)
            genShadow();
        if (orderOnY)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (transform.localPosition.y + offset) / 100f);
        if (generateShadow && !shadow)
            genShadow();
        if (!generateShadow && shadow)
        {
            DestroyImmediate(shadow);
            shadow = null;
        }
    }
    void genShadow()
    {
        refresh = false;
        if (!shadow)
        {
            shadow = new GameObject();
            shadow.name = "shadow";
            shadow.transform.SetParent(gameObject.transform);
            shadow.AddComponent<SpriteRenderer>();
        }
        SpriteRenderer shadowSr = shadow.GetComponent<SpriteRenderer>();
        shadowSr.sprite = sr.sprite;
        shadowSr.color = new Color(0f, 0f, 0f, shadowDarkness);
        shadow.transform.localPosition = Vector3.zero;
        shadow.transform.localScale = new Vector3(1f, shadowScale, 1f);
        float dMin = sr.bounds.extents.y - shadowSr.bounds.extents.y;
        shadow.transform.localPosition = new Vector3(0f, -dMin + shadowOffset, 0f);
    }
}
