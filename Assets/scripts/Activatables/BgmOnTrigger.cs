using UnityEngine;
using System.Collections;

public class BgmOnTrigger : Activatable
{
    public AudioClip bgm;
    public override void init()
    {
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        activate(CharCtrl.script);
        Destroy(gameObject);
    }
    public override void activate(CharCtrl player)
    {
        if (chainedActivatable)
            chainedActivatable.activate(player);
        SoundManager.script.playBGM(bgm);
    }
}
