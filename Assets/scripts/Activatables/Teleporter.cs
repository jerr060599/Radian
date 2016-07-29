using UnityEngine;
using System.Collections;

public class Teleporter : Activatable
{
    public GameObject destination = null;
    public string turnOnAnimation = "", turnOffAnimation = "";
    public Vector2 offset = Vector2.zero;
    public override void init()
    {
    }
    public override void activate(CharCtrl player)
    {
        if (nextActivatable != null)
            nextActivatable.activate(player);
        player.transform.position = destination.transform.position + (Vector3)offset;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (GetComponent<Animator>() && turnOnAnimation.Length != 0)
        {
            GetComponent<Animator>().Play(turnOnAnimation, 0);
            if (destination)
                destination.GetComponent<Animator>().Play(turnOnAnimation, 0);
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (GetComponent<Animator>() && turnOffAnimation.Length != 0)
        {
            GetComponent<Animator>().Play(turnOffAnimation, 0);
            if (destination)
                destination.GetComponent<Animator>().Play(turnOffAnimation, 0);
        }
    }
}
