using UnityEngine;
using System.Collections;

public class Earthquake : Activatable
{
    public float period = 0.1f, duration = 2f, amplitude = 1f;
    float pTimer = 0f;
    public GameObject prefab;
    public override void init()
    {

    }
    void Update()
    {
        if (activated)
        {
            duration -= Time.deltaTime;
            pTimer += Time.deltaTime;
            if (pTimer >= period)
            {
                CameraMovement.script.shake(amplitude);
                pTimer = 0;
            }
            if (duration <= 0f)
                Destroy(gameObject);
        }
    }
    public override void activate(CharCtrl player)
    {
        if (activated)
            return;
        if (chainedActivatable)
            chainedActivatable.activate(player);
        Instantiate(prefab, transform.position, Quaternion.identity);
        activated = true;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
            activate(CharCtrl.script);
    }
}
