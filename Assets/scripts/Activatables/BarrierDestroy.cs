using UnityEngine;

public class BarrierDestroy : Activatable
{
    public float destroyAfter = 2f, lightFade = 0.7f;
    float killTimer = float.PositiveInfinity;
    Light[] larr = null;
    AudioSource sauce = null;
    public override void init()
    {
        larr = GetComponentsInChildren<Light>();
        sauce = GetComponent<AudioSource>();
        Debug.Log(larr.Length);
    }
    public override void activate(CharCtrl player)
    {
		GetComponent<Collider2D> ().enabled = false;
        killTimer = destroyAfter;
        foreach (LightFlicker lf in GetComponentsInChildren<LightFlicker>())
            lf.enabled = false;
        var e = GetComponentInChildren<ParticleSystem>().emission;
        e.enabled = false;
        if (chainedActivatable != null)
            chainedActivatable.activate(player);
    }
    void FixedUpdate()
    {
        killTimer -= Time.fixedDeltaTime;
        if (killTimer <= 0f)
            Destroy(gameObject);
        else if (killTimer <= destroyAfter)
        {
            sauce.volume *= lightFade;
            foreach (Light l in larr)
                l.intensity *= lightFade;
        }
    }
}
