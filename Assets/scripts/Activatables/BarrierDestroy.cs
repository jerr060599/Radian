using UnityEngine;

public class BarrierDestroy : Activatable
{
    public float destroyAfter = 2f;
    float killTimer = float.PositiveInfinity;
    public override void init() { }
    public override void activate(CharCtrl player)
    {
        killTimer = destroyAfter;
        var e = GetComponentInChildren<ParticleSystem>().emission;
        e.enabled = false;
        if (chainedActivatable != null)
            chainedActivatable.activate(player);
    }
    void Update()
    {
        killTimer -= Time.deltaTime;
        if (killTimer <= 0f)
            Destroy(gameObject);
    }
}
