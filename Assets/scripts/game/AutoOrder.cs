using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AutoOrder : MonoBehaviour
{
    public float offset = 0f;
    public bool isStatic = false;
    void Update()
    {
        if (isStatic && !(Application.isEditor && !Application.isPlaying))
            Destroy(this);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (transform.localPosition.y + offset) / 100f);
    }
}
