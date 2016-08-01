using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemy : MonoBehaviour
{
    public static readonly int DEFAULT_DAMAGE = 0, MELEE_DAMAGE = 1, RANGED_DAMAGE = 2;
    public int health = 100;
    public float walkSpeed = 3f, damageKB = 300f;
    public GameObject deathRegistry = null;
    public bool agro = false, hitThisUpdate = false;
    public Rigidbody2D pysc = null;
    public SpriteRenderer sr = null;
    public Animator ani = null;
    bool fading = false;
    void Start()
    {
        pysc = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }
    public virtual void damage(int d, int damageType = 0)
    {
        if (hitThisUpdate)
            return;
        hitThisUpdate = true;
        health -= d;
        if (damageType == MELEE_DAMAGE)
            pysc.AddForce((pysc.position - CharCtrl.script.pysc.position).normalized * damageKB);
        if (health <= 0)
            kill(damageType);
    }
    public virtual void kill(int damageType = 0)
    {
        Destroy(gameObject);
    }
    void OnGUI()
    {
        if (fading)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color *= 0.9f;
            if (GetComponent<SpriteRenderer>().color.a <= 0.05f)
                Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        hitThisUpdate = false;
    }
    public void fadeAndDespawn()
    {
        if (fading)
            return;
        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;
        fading = true;
        if (deathRegistry)
            deathRegistry.GetComponent<DeathRegistry>().registerDeath();
    }
}
