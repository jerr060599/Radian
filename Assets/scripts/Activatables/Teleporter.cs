using UnityEngine;
using System.Collections;

public class Teleporter : Activatable
{
    public GameObject destination = null;
    public string turnOnAnimation = "", turnOffAnimation = "";
    public override void init()
    {
    }
    public override void activate(CharCtrl player)
    {
        if (nextActivatable != null)
            nextActivatable.activate(player);
        player.transform.position = destination.transform.position;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (GetComponent<Animator>() && turnOnAnimation.Length != 0)
            GetComponent<Animator>().Play(turnOnAnimation, 0);
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (GetComponent<Animator>() && turnOffAnimation.Length != 0)
            GetComponent<Animator>().Play(turnOffAnimation, 0);
    }
}
