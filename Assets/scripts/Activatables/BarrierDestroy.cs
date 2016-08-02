using UnityEngine;

public class BarrierDestroy : Activatable
{
    public float destroyAfter = 2f;
    float killTimer = float.PositiveInfinity;
    void Start()
    {

    }
    public override void activate(CharCtrl player)
    {
        killTimer = destroyAfter;
        var e = GetComponent<ParticleSystem>().emission;
        e.enabled = false;
    }
    void Update()
    {
        killTimer -= Time.deltaTime;
        if (killTimer <= 0f)
            Destroy(gameObject);
    }
}
