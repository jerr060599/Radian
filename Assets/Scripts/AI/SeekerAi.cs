using UnityEngine;
using System.Collections;

public class SeekerAi : BasicEnemy
{
    public float agroRadius = 2f, parkRadius = 1.1f, maxImpulse = 1f, atkDistance = 1f, atkDamage = 0.3f;
    public float deathTime = 2f, stunTime = 1.5f, atkWindUp = 0.5f, dashLerp = 0.1f;
    float deathTimer = float.PositiveInfinity, stunTimer = 0f, atkTimer = 0f;
    bool atking = false;
    Vector2 dPos, dashPos;
    protected void Update()
    {
        uiUpdate();
        dPos = CharCtrl.script.pysc.position - pysc.position;
        deathTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
        if (deathTimer <= 0f)
            fadeAndDespawn();
        if (!agro)
        {
            agro = dPos.sqrMagnitude <= agroRadius * agroRadius;
            if (agro)
                SoundManager.script.playOn(transform, SoundManager.script.batAgro, 1f);
        }
        if (agro && deathTimer > deathTime)
        {
            if (dPos.sqrMagnitude > parkRadius * parkRadius)
            {
                if (stunTimer <= 0f)
                {
                    pysc.AddForce(Vector2.ClampMagnitude(((CharCtrl.script.pysc.position - pysc.position).normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    ani.Play(dPos.x <= 0 ? "EnemyWalk" : "EnemyWalkFlipped");
                }
                else
                    pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                atkTimer = 0f;
            }
            else
            {
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                if (atkTimer == 0f)
                    ani.Play(dPos.x < 0f ? "EnemyIdle" : "EnemyIdleFlipped");
                if (!atking)
                    atkTimer += Time.deltaTime;
                if (atkTimer >= atkWindUp)
                {
                    atking = true;
                    atkTimer = 0.00001f;
                    ani.Play(dPos.x < 0f ? "EnemyMelee" : "EnemyMeleeFlipped");
                    SoundManager.script.playOn(transform, SoundManager.script.eSwipe, 1f);
                    dashPos = dPos;
                }
            }
            if (atking && dashPos.x * dashPos.x + dashPos.y * dashPos.y < 0.1)
            {
                atking = false;
                dashPos = Vector2.zero;
                if (dPos.sqrMagnitude < atkDistance * atkDistance)
                    CharCtrl.script.damage(atkDamage);
            }
        }
        else
        {
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
            if (stunTimer < 0f && deathTimer > deathTime)
                ani.Play(dPos.x < 0f ? "EnemyIdle" : "EnemyIdleFlipped");
        }
        transform.position += (Vector3)dashPos * dashLerp;
        dashPos *= 1 - dashLerp;
    }
    public override void kill(int damageType = 0)
    {
        if (deathTimer <= deathTime)
            return;
        if (damageType == MELEE_DAMAGE || damageType == 0)
            ani.Play(dPos.x <= 0 ? "EnemyMeleeDeath" : "EnemyMeleeDeathFlipped");
        else
            ani.Play(dPos.x <= 0 ? "EnemyArrowDeath" : "EnemyArrowDeathFlipped");
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
            ani.Play(dPos.x <= 0 ? "EnemyStagger" : "EnemyStaggerFlipped");
            stunTimer = stunTime;
        }
        base.damage(amount, damageType);
    }
}
