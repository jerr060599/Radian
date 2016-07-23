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
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y + offset) / 100f);
    }
}
