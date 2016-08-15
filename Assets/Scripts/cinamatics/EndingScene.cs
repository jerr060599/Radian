using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EndingScene : MonoBehaviour
{
    public GameObject scene1, scene2;
    float t, a;
    public GameObject canvas, canvas2,canvas3;
    public GameObject brother;
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
        
			if (canvas2!=null)
                canvas2.SetActive(true);
         
        }
		if (a < -31.4 && a > -91.5) {
			scene2.SetActive(false);
			brother.SetActive(false);
			canvas2.SetActive(false);

			if (canvas3!=null)
				canvas3.SetActive(true);
		}
		if (a < -91.5)
			SceneManager.LoadScene ("menu");
    }
}
