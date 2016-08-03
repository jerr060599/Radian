using UnityEngine;
using System.Collections;

public class DelayedActivate : Activatable
{
    public float delay = 10f;
    public override void init()
    { }
    public override void activate(CharCtrl player)
    {
        activated = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                chainedActivatable.activate(CharCtrl.script);
                Destroy(gameObject);
            }
        }
    }
}
