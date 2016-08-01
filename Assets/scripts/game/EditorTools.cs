using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class EditorTools : MonoBehaviour
{
    public bool resetPlayerPref = false;
    public bool clearPrefOnStart = false;
    void Update()
    {
        if (!Application.isEditor || Application.isPlaying)
        {
            if (clearPrefOnStart)
                PlayerPrefs.DeleteAll();
            Destroy(this);
        }
        transform.position = Vector3.zero;
        if (resetPlayerPref)
            PlayerPrefs.DeleteAll();
        resetPlayerPref = false;
    }
}
