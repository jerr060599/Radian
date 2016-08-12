using UnityEngine;

public class BarrierDestroy : Activatable
{
    public float destroyAfter = 2f, lightFade = 0.7f;
    float killTimer = float.PositiveInfinity;
    Light[] larr = null;
    public override void init()
    {
        larr = GetComponentsInChildren<Light>();
        Debug.Log(larr.Length);
    }
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
        foreach (Light l in larr)
            l.intensity *= lightFade;
        if (killTimer <= 0f)
            Destroy(gameObject);
    }
}
