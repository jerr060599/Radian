using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    public static readonly int DEFAULT_DAMAGE = 0, MELEE_DAMAGE = 1, RANGED_DAMAGE = 2;
    public int health = 100;
    public float walkSpeed = 3f;
    public bool agro = false;
    public Rigidbody2D pysc = null;
    bool fading = false;
    void Start()
    {
        pysc = GetComponent<Rigidbody2D>();
    }
    public virtual void damage(int d, int damageType = 0)
    {
        health -= d;
        if (health <= 0)
            kill(damageType);
    }
    public virtual void kill(int damageType = 0)
    {
        Debug.Log("Thou hath killed");
        Destroy(gameObject);
    }
    void OnGUI()
    {
        if (fading)
        {
            GetComponent<SpriteRenderer>().color *= 0.1f;
            if (GetComponent<SpriteRenderer>().color.a <= 0.1f)
                Destroy(gameObject);
        }
    }
    public void fadeAndDespawn()
    {
        fading = true;
    }
}
