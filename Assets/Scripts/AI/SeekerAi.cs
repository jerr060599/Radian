using UnityEngine;
using System.Collections;

public class SeekerAi : BasicEnemy
{
    public float agroRadius = 2f, parkRadius = 1.1f, maxImpulse = 1f;
    float timer = 1.5f;
    public float deathTime = 2f, stunTime = 1.5f, atkWindUp = 0.5f;
    float deathTimer = float.PositiveInfinity, stunTimer = 0f, atkTimer = 0f;
    Vector2 dPos;
    void Start()
    {
        pysc = GetComponent<Rigidbody2D>();
    }
    protected void Update()
    {
        dPos = CharCtrl.script.pysc.position - pysc.position;
        deathTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
        if (deathTimer <= 0f)
            Destroy(gameObject);
        agro = agro ? true : (CharCtrl.script.pysc.position - pysc.position).sqrMagnitude <= agroRadius * agroRadius;
        if (agro && deathTimer > deathTime)
        {
            if (dPos.sqrMagnitude > parkRadius * parkRadius)
                if (stunTimer <= 0f)
                {
                    pysc.AddForce(Vector2.ClampMagnitude(((CharCtrl.script.pysc.position - pysc.position).normalized * walkSpeed - pysc.velocity) * pysc.mass, maxImpulse), ForceMode2D.Impulse);
                    GetComponent<Animator>().Play(dPos.x <= 0 ? "EnemyWalk" : "EnemyWalkFlipped");
                }
                else
                    pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
            else
            {
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
                atkTimer += Time.deltaTime;
                if (atkTimer >= atkWindUp)
                {
                    CharCtrl.script.damage(0.10f);
                    atkTimer = 0f;
                    GetComponent<Animator>().Play(dPos.x < 0f ? "EnemyMelee" : "EnemyMeleeFlipped");
                }
            }
        }
        else
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
    }
    public override void kill(int damageType = 0)
    {
        if (damageType == MELEE_DAMAGE || damageType == 0)
            GetComponent<Animator>().Play(dPos.x <= 0 ? "EnemyMeleeDeath" : "EnemyMeleeDeathFlipped");
        else
            GetComponent<Animator>().Play(dPos.x <= 0 ? "EnemyArrowDeath" : "EnemyArrowDeathFlipped");
        deathTimer = deathTime;
    }
    public override void damage(int amount, int damageType = 0)
    {
        base.damage(amount, damageType);
        atkTimer = 0f;
        if (deathTimer > deathTime)
        {
            GetComponent<Animator>().Play(dPos.x <= 0 ? "EnemyStagger" : "EnemyStaggerFlipped");
            stunTimer = stunTime;
        }
    }
}
