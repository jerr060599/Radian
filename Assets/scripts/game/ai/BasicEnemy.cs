using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    public int health = 100;
    public float walkSpeed = 3f;
    public bool agro = false;
    public Rigidbody2D pysc = null;
    void Start()
    {
        pysc = GetComponent<Rigidbody2D>();
    }
    public virtual void damage(int d)
    {
        health -= d;
        if (health <= 0)
            kill();
    }
    public virtual void kill()
    {
        Debug.Log("Thou hath killed");
    }
}
