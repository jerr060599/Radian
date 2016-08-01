using UnityEngine;
using System.Collections;

public class Tamahto : BasicEnemy
{
    public readonly int SLAM = 0, CRUSH = 1, WINDUP = 2;
    public float agroRadius = 2f, parkRadius = 4f, maxImpulse = 1f, atkDistance = 1f, atkDamage = 0.2f, crushAirTime = 1f;
    public float deathTime = 2f, stunTime = 1.5f, atkWindUp = 0.5f, dashLerp = 0.1f;
    float deathTimer = float.PositiveInfinity, stunTimer = 0f, atkTimer = 0f;
    int atking = -1;
    Vector2 dPos, dashPos;
    protected void Update()
    {
        dPos = CharCtrl.script.pysc.position - pysc.position;
        deathTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
        if (deathTimer <= 0f)
            fadeAndDespawn();
        agro = agro ? true : (CharCtrl.script.pysc.position - pysc.position).sqrMagnitude <= agroRadius * agroRadius;
        if (agro && deathTimer > deathTime)
        {
            if (dPos.sqrMagnitude > parkRadius * parkRadius && atkTimer == 0f)
            {
                if (stunTimer <= 0f)
                {
                    pysc.AddForce(Vector2.ClampMagnitude(((CharCtrl.script.pysc.position - pysc.position).normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    ani.Play(dPos.x <= 0 ? "walk" : "walkFlipped");
                }
                else
                {
                    pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    ani.Play(dPos.x < 0f ? "idle" : "idleFlipped");
                }
                atkTimer = 0f;
            }
            else
            {
                if (atking != CRUSH)
                    pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                if (atking == -1)
                {
                    dashPos = dPos + CharCtrl.script.pysc.velocity * atkWindUp;
                    atking = WINDUP;
                }
                atkTimer += Time.deltaTime;
                if (atkTimer >= atkWindUp)
                {
                    atking = Random.value > 0.0f ? SLAM : CRUSH;
                    if (atking == CRUSH)
                    {
                        pysc.GetComponent<Rigidbody2D>().velocity = new Vector2(dashPos.x / crushAirTime,
                            (dashPos.y - Physics2D.gravity.y * (pysc.GetComponent<Rigidbody2D>().gravityScale) * crushAirTime * crushAirTime / 2) / crushAirTime);
                        dashPos = Vector2.zero;
                    }
                    ani.Play(dPos.x < 0f ? "atk" : "atkFlipped");
                    atkTimer = 0f;
                }
            }
        }
        else
        {
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
            if (stunTimer < 0f && deathTimer > deathTime)
                ani.Play(dPos.x < 0f ? "idle" : "idleFlipped");
        }
        if (atking != -1 && atking != WINDUP)
        {
            transform.position += (Vector3)dashPos * dashLerp;
            dashPos *= 1 - dashLerp;
            if (dashPos.x * dashPos.x + dashPos.y * dashPos.y < 1.0f)
            {
                atking = -1;
                dashPos = Vector2.zero;
                if (dPos.sqrMagnitude < atkDistance * atkDistance)
                    CharCtrl.script.damage(atkDamage);
            }
        }
    }
    public override void kill(int damageType = 0)
    {
        ani.Play(dPos.x <= 0 ? "death" : "deathFlipped");
        deathTimer = deathTime;
        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;
    }
    public override void damage(int amount, int damageType = 0)
    {
        atkTimer = 0f;
        agro = true;
        if (deathTimer > deathTime)
        {
            ani.Play(dPos.x <= 0 ? "stagger" : "staggerFlipped");
            stunTimer = stunTime;
        }
        base.damage(amount, damageType);
    }
}
