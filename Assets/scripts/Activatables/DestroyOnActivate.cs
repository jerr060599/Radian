using UnityEngine;
using System.Collections;

public class DestroyOnActivate : Activatable
{
    public override void activate(CharCtrl player)
    {
        Destroy(gameObject);
    }
}
