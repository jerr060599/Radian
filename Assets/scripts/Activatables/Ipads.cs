using UnityEngine;
using System.Collections;

public class Ipads : Activatable
{
    public Canvas canvas;
    public string content;
    UnityEngine.UI.Text txt;
    float a = 0f;
    bool showing = false;
    Color c = new Color(1, 1, 1, 0);
    CanvasRenderer cr;
    void Start()
    {
        txt = canvas.GetComponentInChildren<UnityEngine.UI.Text>();
        cr = canvas.GetComponent<CanvasRenderer>();
        if (chainedActivatable != null)
            nextActivatable = chainedActivatable.GetComponent<Activatable>();
    }
    public override void activate(CharCtrl player)
    {
        if (nextActivatable != null)
            nextActivatable.activate(player);
        if (showing)
            CharCtrl.script.invulnerable = false;
        else
        {
            CharCtrl.script.invulnerable = true;
            txt.text = content;
        }
        showing = !showing;
    }
    void Update()
    {
        if (canvas.enabled)
        {
            if (showing)
            {
                a += (1 - a) * 0.1f;
                cr.SetAlpha(a);
            }
            else
            {
                a -= a * 0.1f;
                cr.SetAlpha(a);
            }
        }
        if (a < 0.05f)
            canvas.enabled = false;
        else
            canvas.enabled = true;

    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (showing && c.gameObject == CharCtrl.script.gameObject)
            activate(CharCtrl.script);
    }
}
