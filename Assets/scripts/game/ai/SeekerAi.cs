using UnityEngine;
using System.Collections;

public class SeekerAi : BasicEnemy
{
    public float agroRadius = 2f, parkRadius = 1.1f, maxImpulse = 1f;
    // Update is called once per frame
    protected void Update()
    {
        agro = (CharCtrl.script.pysc.position - pysc.position).sqrMagnitude <= agroRadius * agroRadius;
        if (agro)
        {
            Vector2 dPos = CharCtrl.script.pysc.position - pysc.position;
            if (dPos.sqrMagnitude > parkRadius * parkRadius)
                pysc.AddForce(Vector2.ClampMagnitude(((CharCtrl.script.pysc.position - pysc.position).normalized * walkSpeed - pysc.velocity) * pysc.mass, maxImpulse), ForceMode2D.Impulse);
            else
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
        }
        else
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
    }

    public override void kill()
    {
        Destroy(gameObject);
    }
}
