using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float damage;
    public bool turnWithVel = false;
    public bool enemyProjectile = false;
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.gameObject.name);
        if (c.attachedRigidbody && c.attachedRigidbody.gameObject)
        {
            if (enemyProjectile)
            {
                if (c.attachedRigidbody == CharCtrl.script.pysc)
                    CharCtrl.script.damage(damage);
            }
            else
            {
                BasicEnemy be = c.attachedRigidbody.gameObject.GetComponent<BasicEnemy>();
                if (be)
                    be.damage((int)damage, BasicEnemy.RANGED_DAMAGE);
            }
        }
        Destroy(gameObject);
    }
    public void setVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        transform.localRotation = Quaternion.LookRotation(Vector3.forward, -new Vector3(velocity.y, -velocity.x));
    }
    void Update()
    {
        if (turnWithVel)
            transform.localRotation = Quaternion.LookRotation(Vector3.forward, -new Vector3(GetComponent<Rigidbody2D>().velocity.y, -GetComponent<Rigidbody2D>().velocity.x));
    }
}
