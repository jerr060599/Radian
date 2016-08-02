using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemy : MonoBehaviour
{
    public static readonly int DEFAULT_DAMAGE = 0, MELEE_DAMAGE = 1, RANGED_DAMAGE = 2;
    public int maxHealth = 100, health = 100, healthPerHc = 5;
    public float walkSpeed = 3f, damageKB = 300f, hcSpacing = 1f, hcYOffset = 1f, healthFadeLerp = 0.1f;
    public GameObject deathRegistry = null, healthCrystal = null;
    public bool agro = false, hitThisUpdate = false;
    public Rigidbody2D pysc = null;
    public SpriteRenderer sr = null;
    public Animator ani = null;
    GameObject[] hcs = null;
    SpriteRenderer[] hcSrs = null;
    int hcCount = 0;
    bool fading = false, healthShowing = false;
    Color curHcC = new Color(1, 1, 1, 0);
    float heathShowTimer = 0f;
    void Start()
    {
        pysc = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        hcs = new GameObject[maxHealth / healthPerHc];
        hcSrs = new SpriteRenderer[hcs.Length];
        hcCount = hcs.Length;
        float curX = -0.5f * (hcs.Length - 1) * hcSpacing;
        for (int i = 0; i < hcs.Length; i++, curX += hcSpacing)
        {
            hcs[i] = Instantiate(healthCrystal, new Vector3(transform.position.x + curX, transform.position.y + hcYOffset, transform.position.z), Quaternion.identity) as GameObject;
            hcs[i].transform.SetParent(transform);
            hcSrs[i] = hcs[i].GetComponent<SpriteRenderer>();
            hcSrs[i].color = curHcC;
            hcs[i].SetActive(false);
        }
    }
    public virtual void damage(int d, int damageType = 0)
    {
        if (hitThisUpdate)
            return;
        hitThisUpdate = true;
        health -= d;
        hcCount = Mathf.Max(health, 0) / healthPerHc;
        heathShowTimer = 2f;
        for (int i = hcCount; i < hcs.Length; i++)
            if (hcs[i].activeSelf)
                hcs[i].SetActive(false);
        if (damageType == MELEE_DAMAGE)
            pysc.AddForce((pysc.position - CharCtrl.script.pysc.position).normalized * damageKB);
        if (health <= 0)
            kill(damageType);
    }
    public virtual void kill(int damageType = 0)
    {
        Destroy(gameObject);
    }
    public void uiUpdate()
    {
        heathShowTimer -= Time.deltaTime;
        if (heathShowTimer > 0f)
        {
            curHcC.a += (1 - curHcC.a) * healthFadeLerp;
            for (int i = 0; i < hcCount; i++)
                hcSrs[i].color = curHcC;
        }
        else
        {
            curHcC.a -= curHcC.a * healthFadeLerp;
            for (int i = 0; i < hcCount; i++)
                hcSrs[i].color = curHcC;
        }
        if (curHcC.a > 0.05)
        {
            if (!healthShowing)
            {
                for (int i = 0; i < hcCount; i++)
                    hcs[i].SetActive(true);
                healthShowing = true;
            }
        }
        else if (healthShowing)
        {
            for (int i = 0; i < hcCount; i++)
                hcs[i].SetActive(false);
            healthShowing = false;
        }
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
