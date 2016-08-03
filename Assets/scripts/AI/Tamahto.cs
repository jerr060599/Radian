using UnityEngine;
using System.Collections;

public class Tamahto : BasicEnemy
{
    public readonly int SLAMING = 0, CRUSHING = 1, WINDING = 2, DYING = 3, SEEKING = 4, IDLE = 5, STUN = 6, FADING = 7;
    public float agroRadius = 2f, parkRadius = 4f, maxImpulse = 1f, atkDistance = 1f, atkDamage = 0.2f, crushAirTime = 1f;
    public float deathTime = 2f, stunTime = 1.5f, atkWindUp = 0.5f, dashLerp = 0.1f, dashW = 0.5f;
    float timer = 0f;
    int curState = 5;
    GameObject shadow;
    Vector2 dPos, tmp, lastCCPos = Vector2.zero;
    public override void init()
    {
        shadow = GetComponent<AutoOrder>().shadow;
    }
    protected void FixedUpdate()
    {
        uiUpdate();
        timer -= Time.fixedDeltaTime;
        dPos = CharCtrl.script.pysc.position - pysc.position;
        switch (curState)
        {
            case 6:
                ani.Play(dPos.x < 0 ? "stagger" : "staggerFlipped", 0);
                if (timer <= 0f)
                    curState = SEEKING;
                break;
            case 5:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                ani.Play("idle");
                if (dPos.sqrMagnitude < agroRadius * agroRadius)
                    curState = SEEKING;
                break;
            case 4:
                if (dPos.sqrMagnitude > parkRadius * parkRadius)
                {
                    pysc.AddForce(Vector2.ClampMagnitude(((CharCtrl.script.pysc.position - pysc.position).normalized * walkSpeed - pysc.velocity), maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                    ani.Play(dPos.x <= 0 ? "walk" : "walkFlipped");
                }
                else
                {
                    lastCCPos = dPos;
                    curState = WINDING;
                    timer = atkWindUp;
                }
                break;
            case 3:
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                if (timer <= 0f)
                {
                    fadeAndDespawn();
                    curState = FADING;
                }
                break;
            case 2:
                ani.Play(lastCCPos.x < 0f ? "atk" : "atkFlipped");
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity, maxImpulse) * pysc.mass, ForceMode2D.Impulse);
                if (timer <= 0f)
                {
                    curState = Random.value > 0.55f ? SLAMING : CRUSHING;
                    shadow.SetActive(false);
                    if (curState == CRUSHING)
                    {
                        timer = crushAirTime;
                        pysc.gravityScale = 2f;
                        foreach (Collider2D c in GetComponents<Collider2D>())
                            c.enabled = false;
                        lastCCPos += CharCtrl.script.pysc.velocity * crushAirTime;
                        pysc.GetComponent<Rigidbody2D>().velocity = new Vector2(lastCCPos.x / crushAirTime,
                            (lastCCPos.y - Physics2D.gravity.y * (pysc.GetComponent<Rigidbody2D>().gravityScale) * crushAirTime * crushAirTime / 2) / crushAirTime);
                    }
                    else
                        tmp = lastCCPos;
                }
                break;
            case 1:
                if (timer <= 0)
                {
                    pysc.gravityScale = 0f;
                    pysc.velocity = Vector2.zero;
                    foreach (Collider2D c in GetComponents<Collider2D>())
                        c.enabled = true;
                    if (dPos.sqrMagnitude < atkDistance * atkDistance)
                        CharCtrl.script.damage(atkDamage);
                    CameraMovement.script.shake(0.5f);
                    curState = SEEKING;
                    shadow.SetActive(true);
                }
                break;
            case 0:
                transform.position += (Vector3)tmp * dashLerp;
                tmp *= 1 - dashLerp;
                if (pysc.rotation < 0)
                    pysc.rotation = pysc.rotation % 360 + 360;
                else
                    pysc.rotation = pysc.rotation % 360;
                if (tmp.x * tmp.x + tmp.y * tmp.y < 0.2f && Mathf.Min(360 - pysc.rotation, pysc.rotation) < 10)
                {
                    pysc.rotation = 0f;
                    tmp = Vector2.zero;
                    if (dPos.sqrMagnitude < atkDistance * atkDistance)
                        CharCtrl.script.damage(atkDamage);
                    curState = SEEKING;
                    shadow.SetActive(true);
                }
                else
                    pysc.rotation += tmp.x > 0 ? -dashW : dashW;
                break;
        }
    }
    public override void kill(int damageType = 0)
    {
        ani.Play(lastCCPos.x <= 0 ? "death" : "deathFlipped");
        timer = deathTime;
        curState = DYING;
        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;
    }
    public override void damage(int amount, int damageType = 0)
    {
        if (curState != CRUSHING)
        {
            curState = STUN;
            timer = stunTime;
        }
        base.damage(amount, damageType);
    }
}
