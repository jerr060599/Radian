using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class EditorTools : MonoBehaviour
{
    public bool resetPlayerPref = false;
    void Update()
    {
        if (!Application.isEditor || Application.isPlaying)
        {
            Destroy(this);
        }
        transform.position = Vector3.zero;
        if (resetPlayerPref)
            PlayerPrefs.DeleteAll();
        resetPlayerPref = false;
    }
}
