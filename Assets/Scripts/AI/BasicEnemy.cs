using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    public static readonly int DEFAULT_DAMAGE = 0, MELEE_DAMAGE = 1, RANGED_DAMAGE = 2;
    public int health = 100;
    public float walkSpeed = 3f;
    public bool agro = false;
    public Rigidbody2D pysc = null;
    void Start()
    {
        pysc = GetComponent<Rigidbody2D>();
    }
    public virtual void damage(int d, int damageType = 0)
    {
        health -= d;
        if (health <= 0)
            kill();
    }
    public virtual void kill()
    {
        Debug.Log("Thou hath killed");
		Destroy (gameObject);
    }
}
