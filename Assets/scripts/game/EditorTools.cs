using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class EditorTools : MonoBehaviour
{
    public bool resetPlayerPref = false;
    void Start()
    {
        Application.targetFrameRate = 65;
    }
    void Update()
    {
        if (!Application.isEditor || Application.isPlaying)
        {
            Destroy(this);
        }
        else
        {
            transform.position = Vector3.zero;
            if (resetPlayerPref)
                PlayerPrefs.DeleteAll();
            resetPlayerPref = false;
        }
    }
}
