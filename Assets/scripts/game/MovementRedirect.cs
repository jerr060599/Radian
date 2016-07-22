using UnityEngine;
using System.Collections;

public class MovementRedirect : MonoBehaviour
{
    public Vector2 dir;
    void Start()
    {
        dir = dir.normalized;
    }
}
