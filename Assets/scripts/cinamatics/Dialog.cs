using UnityEngine;
using System.Collections;

public class Dialog : Activatable
{
    public Sprite[] diags;
    SpriteRenderer sr;
    int index = 0;
    public override void init()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public override void activate(CharCtrl player)
    {
        sr.sprite = diags[index];
        index = Mathf.Min(index + 1, diags.Length - 1);
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
        {
            sr.sprite = null;
            index = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
        {
            sr.sprite = diags[0];
            index = Mathf.Min(1, diags.Length - 1);
        }
    }
}
