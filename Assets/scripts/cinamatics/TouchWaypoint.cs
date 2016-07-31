﻿using UnityEngine;
using System.Collections;

public class TouchWaypoint : Waypoint
{
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
            use();
    }
}
