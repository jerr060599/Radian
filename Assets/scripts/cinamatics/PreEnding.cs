using UnityEngine;
using System.Collections;

public class PreEnding : Activatable
{
    public GameObject tablet, boss;
    public float timeToIpad = 10f;
    public override void init()
    {
    }
    public override void activate(CharCtrl player)
    {
        if (chainedActivatable)
            chainedActivatable.activate(player);
        tablet.transform.position = boss.transform.position;
        tablet.SetActive(true);
    }
}
