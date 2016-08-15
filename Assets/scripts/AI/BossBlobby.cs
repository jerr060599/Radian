using UnityEngine;
using System.Collections;

public class BossBlobby : BasicEnemy
{
    public static readonly int SEEKING = 0, STOMPING = 1, IDLING = 2, VOMIT_WINDING = 3, VOMITING = 4, STUN = 5, AVOIDING = 6, MOUTH_OPENING = 7, DYING = 8, FADING = 9;
    public float range = 10f, projectileAirTime = 1f, avoidDistance = 3f, maxImpulse = 1f, vomitFireRate = 0.5f, seekDistance = 8f, volleyLength = 4f, atkAnimationLength = 1.2f, volleySpacing = 3f, deathTime = 1f, stompRadius = 2f, stompDamage = 0.3f, stompWindup = 1f, stompAirTime = 2f;
    float timer = 0f, timer2 = 0f;
    public GameObject projectile;
    Vector2 dPos;
    int curState = IDLING;
    GameObject shadow;
    public override void init()
    {
        shadow = GetComponent<AutoOrder>().shadow;
    }
    public void fire(Transform t)
    {
        Vector2 dPos = Vector2.ClampMagnitude((Vector2)(t.position) - pysc.position, range);
        GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.AngleAxis(-90f, Vector3.forward));
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(dPos.x / projectileAirTime,
            (dPos.y - Physics2D.gravity.y * (projectile.GetComponent<Rigidbody2D>().gravityScale) * projectileAirTime * projectileAirTime / 2) / projectileAirTime);
        proj.GetComponent<Projectile>().lifeTime = projectileAirTime + 0.1f;
    }

    void Update()
    {
        uiUpdate();
        dPos = pysc.position - CharCtrl.script.pysc.position;
        float d = dPos.sqrMagnitude;
        timer -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        switch (curState)
        {
            case 0:
                pysc.AddForce(Vector2.ClampMagnitude((-dPos.normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play(dPos.x > 0f ? "walk" : "walkFlipped", 0);
                if (d < avoidDistance * avoidDistance)
                {
                    curState = AVOIDING;
                    timer = stompWindup;
                }
                else if (d < seekDistance * seekDistance)
                {
                    timer = atkAnimationLength;
                    curState = MOUTH_OPENING;
                }
                break;
            case 1:
                ani.Play(dPos.x > 0f ? "idle" : "idleFlipped");
                if (timer <= 0f)
                {
                    pysc.gravityScale = 0f;
                    pysc.velocity = Vector2.zero;
                    shadow.SetActive(true);
                    foreach (Collider2D c in GetComponents<Collider2D>())
                        c.enabled = true;
                    if (d < stompRadius * stompRadius)
                        CharCtrl.script.damage(stompDamage);
                    CameraMovement.script.shake(0.5f);
                    curState = SEEKING;
                    SoundManager.script.playOn(transform, SoundManager.script.turtleStomp);
                }
                break;
            case 2:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play("idle");
                if (d < seekDistance * seekDistance)
                    curState = SEEKING;
                break;
            case 3:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play(dPos.x > 0f ? "idle" : "idleFlipped");
                if (timer <= 0f)
                {
                    timer = atkAnimationLength;
                    curState = MOUTH_OPENING;
                }
                if (d > range * range)
                {
                    curState = SEEKING;
                    timer = float.PositiveInfinity;
                }
                if (d < avoidDistance * avoidDistance)
                {
                    curState = AVOIDING;
                    timer = stompWindup;
                }
                break;
            case 4:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play(dPos.x > 0f ? "atk" : "atkFlipped", 0);
                if (d < avoidDistance * avoidDistance)
                {
                    curState = AVOIDING;
                    timer = stompWindup;
                }
                if (timer < 0)
                {
                    curState = VOMIT_WINDING;
                    timer = volleySpacing;
                }
                if (timer2 < 0f)
                {
                    timer2 = vomitFireRate;
                    fire(CharCtrl.script.transform);
                }
                break;
            case 5:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                if (timer <= 0f)
                    curState = SEEKING;
                break;
            case 6:
                pysc.AddForce(Vector2.ClampMagnitude((dPos.normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play(dPos.x < 0f ? "walk" : "walkFlipped", 0);
                if (d > avoidDistance * avoidDistance)
                    curState = SEEKING;
                if (timer <= 0)
                {
                    foreach (Collider2D c in GetComponents<Collider2D>())
                        c.enabled = false;
                    pysc.gravityScale = 2f;
                    dPos *= -1;
                    pysc.GetComponent<Rigidbody2D>().velocity = new Vector2(dPos.x / stompAirTime,
                            (dPos.y - Physics2D.gravity.y * (pysc.GetComponent<Rigidbody2D>().gravityScale) * stompAirTime * stompAirTime / 2) / stompAirTime);
                    curState = STOMPING;
                    SoundManager.script.playOn(transform, SoundManager.script.turtleJump);
                    timer = stompAirTime;
                }
                break;
            case 7:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play(dPos.x > 0f ? "atk" : "atkFlipped", 0);
                if (timer <= 0f)
                {
                    curState = VOMITING;
                    SoundManager.script.playOn(transform, SoundManager.script.blobVomit);
                    timer = volleyLength;
                }
                break;
            case 8:
                if (timer <= 0)
                {
                    if (fading)
                        return;
                    foreach (Collider2D c in GetComponents<Collider2D>())
                        c.enabled = false;
                    fading = true;
                    curState = FADING;
                }
                break;
        }
    }
    public override void kill(int damageType = 0)
    {
        if (curState == FADING || curState == DYING)
            return;
        if (deathRegistry)
            deathRegistry.GetComponent<DeathRegistry>().registerDeath();
        SoundManager.script.playOn(transform, SoundManager.script.blobDeath, 1f);
        curState = DYING;
        timer = deathTime;
        ani.Play(dPos.x > 0f ? "death" : "deathFlipped", 0);
    }
    public override void damage(int d, int damageType = 0)
    {
        base.damage(d, damageType);
        if (curState == IDLING)
            curState = SEEKING;
    }
}
