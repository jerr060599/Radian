using UnityEngine;
using System.Collections;

public class Teleporter : Activatable
{
    GameObject destination = null;
    public override void init()
    {
    }
    public override void activate(CharCtrl player)
    {
        if (nextActivatable != null)
            nextActivatable.activate(player);
        player.transform.position = destination.transform.position;
    }
}
