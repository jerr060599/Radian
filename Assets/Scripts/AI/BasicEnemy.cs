using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemy : MonoBehaviour
{
    public static readonly int DEFAULT_DAMAGE = 0, MELEE_DAMAGE = 1, RANGED_DAMAGE = 2;
    public int health = 100;
    public float walkSpeed = 3f;
    public bool agro = false;
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
        health -= d;
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
    public void fadeAndDespawn()
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
            c.enabled = false;
        foreach (CircleCollider2D c in GetComponents<CircleCollider2D>())
            c.enabled = false;
        foreach (BoxCollider2D c in GetComponents<BoxCollider2D>())
            c.enabled = false;
        fading = true;
    }
}
