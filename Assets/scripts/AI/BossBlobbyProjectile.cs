using UnityEngine;
using System.Collections;

public class BossBlobbyProjectile : Projectile
{
    public float percentOfSpawn = 0.02f;
    public GameObject blobby;
    public override void hitEvent()
    {
        if (percentOfSpawn >= Random.value)
            Instantiate(blobby, transform.position, Quaternion.identity);
    }
}
