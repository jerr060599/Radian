using UnityEngine;
using System.Collections;

public class EndingScene : MonoBehaviour
{
    public GameObject scene1, scene2;
    float t, a;
    public GameObject canvas, canvas2;
    public GameObject brother;
    bool credit = false;
    void Start()
    {
        Cursor.visible = false;
        t = 3f;
        a = t;

    }
    void Update()
    {
        a -= Time.deltaTime;
        if (a < 0.01f && a > -1)
        {
            scene1.SetActive(false);
            scene2.SetActive(true);
        }
        if (a < 0.5f && a > 0 && canvas)
            canvas.SetActive(true);
        if (a < -29.5 && a > -31.5)
        {
            scene2.SetActive(false);
            if (canvas2)
                canvas2.SetActive(true);
            brother.SetActive(false);
            if (!credit)
            {

                credit = true;
            }
        }
    }
}
