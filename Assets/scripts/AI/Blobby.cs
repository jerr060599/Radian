using UnityEngine;
using System.Collections;

public class Blobby : BasicEnemy
{
    public bool big = false;
    public float projectileAirTime = 1f, range = 10f, avoidDistance = 3f, maxImpulse = 1f, atkTime = 0.5f, seekDistance = 8f, atkAnimationLength = 1f, volleyLength = 4f, volleySpacing = 3f, deathTime = 1f;
    float atkTimer = float.PositiveInfinity, atkWinUpTimer = 0f, volleyTimer = 0f, deathTimer = float.PositiveInfinity;
    public GameObject projectile;
    Vector2 dPos;
    public void fire(Transform t)
    {
        atkTimer = float.PositiveInfinity;
        Vector2 dPos = (Vector2)(t.position) - pysc.position;
        GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.AngleAxis(-90f, Vector3.forward));
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(dPos.x / projectileAirTime,
            (dPos.y - Physics2D.gravity.y * (projectile.GetComponent<Rigidbody2D>().gravityScale) * projectileAirTime * projectileAirTime / 2) / projectileAirTime);
        proj.GetComponent<Projectile>().lifeTime = projectileAirTime + 0.1f;
    }

    void Update()
    {
        uiUpdate();
        dPos = pysc.position - CharCtrl.script.pysc.position;
        float d = dPos.x * dPos.x + dPos.y * dPos.y;
        atkTimer -= Time.deltaTime;
        deathTimer -= Time.deltaTime;
        if (!agro)
        {
            agro = d < range * range;
            if (agro && big)
                SoundManager.script.playOn(transform, SoundManager.script.blobAgro);
        }
        if (deathTimer <= 0f)
            fadeAndDespawn();
        if (agro && deathTimer > deathTime)
        {
            if (atkTimer > atkTime)
            {
                if (d < avoidDistance * avoidDistance)
                {
                    pysc.AddForce(Vector2.ClampMagnitude((dPos.normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    ani.Play(dPos.x < 0f ? "walk" : "walkFlipped", 0);
                }
                else if (d > seekDistance * seekDistance)
                {
                    pysc.AddForce(Vector2.ClampMagnitude((-dPos.normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    ani.Play(dPos.x > 0f ? "walk" : "walkFlipped", 0);
                }
                else
                {
                    pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    //if (animationOverride <= 0f)
                    ani.Play(dPos.x > 0f ? "idle" : "idleFlipped", 0);
                }
                atkWinUpTimer = 0;
                volleyTimer = 0;
            }
            else if (volleyTimer < 0f)
            {
                volleyTimer += Time.deltaTime;
                atkWinUpTimer = 0;
                ani.Play(dPos.x > 0f ? "idle" : "idleFlipped", 0);
            }
            else
            {
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play(dPos.x > 0f ? "atk" : "atkFlipped", 0);
                atkWinUpTimer += Time.deltaTime;
                volleyTimer += Time.deltaTime;
            }
            if (volleyTimer >= volleyLength)
            {
                atkWinUpTimer = 0;
                volleyTimer = -volleySpacing;
            }
            if (atkTimer <= 0f && atkWinUpTimer > atkAnimationLength)
                fire(CharCtrl.script.transform);
            if (d < avoidDistance * avoidDistance || d > seekDistance * seekDistance)
                atkTimer = float.PositiveInfinity;
            else if (atkTimer > atkTime)
                atkTimer = d < range * range ? atkTime : float.PositiveInfinity;
        }
        else
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
    }
    public override void kill(int damageType = 0)
    {
        if (deathTimer <= deathTime)
            return;
        SoundManager.script.playOn(transform, SoundManager.script.blobDeath, 1f);
        deathTimer = deathTime;
        ani.Play(dPos.x > 0f ? "death" : "deathFlipped", 0);
    }
    public override void damage(int d, int damageType = 0)
    {
        base.damage(d, damageType);
        agro = true;
    }
}
