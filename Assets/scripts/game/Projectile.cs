using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int damage;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.attachedRigidbody && c.attachedRigidbody.gameObject)
        {
            BasicEnemy be = c.attachedRigidbody.gameObject.GetComponent<BasicEnemy>();
            if (be)
                be.damage(damage, BasicEnemy.RANGED_DAMAGE);
        }
        Destroy(gameObject);
    }
    public void setVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        transform.localRotation = Quaternion.LookRotation(Vector3.forward, -velocity);
    }
}
