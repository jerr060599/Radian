using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ending : Activatable
{
    public GameObject creditCanvas, text;
    public CanvasRenderer[] crs;
    public float scrollSpeed = 0.05f, alphaGain = 0.005f, creditEndY = 1100;
    float curA = 0f;
    public override void init()
    {
        crs = creditCanvas.GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer cr in crs)
            cr.SetAlpha(curA);
    }
    public override void activate(CharCtrl player)
    {
        Time.timeScale = 0f;
        activated = true;
        creditCanvas.SetActive(true);
    }
    void Update()
    {
        if (activated)
        {
            curA = Mathf.Min(1f, curA + alphaGain);
            if (curA != 1f)
                foreach (CanvasRenderer cr in crs)
                    cr.SetAlpha(curA);
            text.transform.position += Vector3.up * scrollSpeed;
            if (creditEndY < text.transform.position.y)
                SceneManager.LoadScene("Menu");
        }
    }
}
