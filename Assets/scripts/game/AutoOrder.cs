using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AutoOrder : MonoBehaviour
{
    public float offset = 0f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y + offset) / 100f);
    }
}
