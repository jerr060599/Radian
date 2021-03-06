﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float damage, fadeAfterHit = 0f, splatTolerance = 1f, lifeTime = 500f, cameraShake = 0f, hitVolume = 1f;
    public bool turnWithVel = false, enemyProjectile = false, splatAtLocation = false;
    public string splatAnimation = "";
    public Vector2 splatLocation = Vector2.zero;
    public AudioClip soundOnHit = null, soundOnNullify = null;
    bool hit = false;
    SpriteRenderer sr = null;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (hit)
            return;
        if (c.isTrigger && !c.gameObject.GetComponent<BasicEnemy>())
            return;
        if (c.attachedRigidbody && c.attachedRigidbody.gameObject)
        {
            if (enemyProjectile)
            {
                if (c.attachedRigidbody == CharCtrl.script.pysc)
                {
                    CharCtrl.script.damage(damage);
                    SoundManager.script.playOn(transform, soundOnHit, hitVolume);
                }
                else
                    SoundManager.script.playOn(transform, soundOnNullify, hitVolume);
            }
            else
            {
                BasicEnemy be = c.attachedRigidbody.gameObject.GetComponent<BasicEnemy>();
                if (be)
                {
                    be.damage((int)damage, BasicEnemy.RANGED_DAMAGE);
                    SoundManager.script.playOn(transform, soundOnHit, hitVolume);
                }
                else
                    SoundManager.script.playOn(transform, soundOnNullify, hitVolume);
            }
        }
        else
            SoundManager.script.playOn(transform, soundOnNullify, hitVolume);
        stop();
    }
    public virtual void hitEvent()
    {
    }
    void stop()
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;
        if (hit)
            return;
        hit = true;
        SoundManager.script.playOn(transform, soundOnNullify, hitVolume);
        if (cameraShake != 0f)
            CameraMovement.script.shake(cameraShake);
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        turnWithVel = false;
        if (GetComponent<Animator>() && splatAnimation.Length != 0)
            GetComponent<Animator>().Play(splatAnimation, 0);
        hitEvent();
    }
    public void setVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        transform.localRotation = Quaternion.LookRotation(Vector3.forward, -new Vector3(velocity.y, -velocity.x));
    }
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            stop();
        if (splatAtLocation)
        {
            Vector2 dPos = new Vector2(transform.position.x - splatLocation.x, transform.position.y - splatLocation.y);
            if (dPos.x * dPos.x + dPos.y + dPos.y <= splatTolerance)
                stop();
        }
        if (turnWithVel)
            transform.localRotation = Quaternion.LookRotation(Vector3.forward, -new Vector3(GetComponent<Rigidbody2D>().velocity.y, -GetComponent<Rigidbody2D>().velocity.x));
        if (hit)
        {
            sr.color *= fadeAfterHit;
            if (GetComponent<SpriteRenderer>().color.a <= 0.05f)
                Destroy(gameObject);
        }
    }
}
