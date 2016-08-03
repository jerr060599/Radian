using UnityEngine;

public class ActivateOnEnter : Activatable
{
    public int validUsage = 1;
    public override void init()
    {
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
            activate(CharCtrl.script);
    }
    public override void activate(CharCtrl player)
    {
        validUsage--;
        if (chainedActivatable != null)
            chainedActivatable.activate(player);
        if (validUsage <= 0)
            Destroy(gameObject);
    }
}
