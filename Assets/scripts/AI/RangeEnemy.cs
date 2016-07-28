using UnityEngine;
using System.Collections;

public class RangeEnemy : BasicEnemy
{
    public float projectileAirTime = 1f, range = 10f, avoidDistance = 3f, maxImpulse = 1f, atkTime = 0.5f;
    float atkTimer = float.PositiveInfinity;
    public GameObject projectile;
    // Use this for initialization
    public void fire(Transform t)
    {
        Debug.Log("Fak u");
        atkTimer = float.PositiveInfinity;
        Vector2 dPos = (Vector2)(t.position) - pysc.position;
        GameObject tmp = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(dPos.x / projectileAirTime,
            (dPos.y - Physics2D.gravity.y * (projectile.GetComponent<Rigidbody2D>().gravityScale) * projectileAirTime * projectileAirTime / 2) / projectileAirTime);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 dPos = pysc.position - CharCtrl.script.pysc.position;
        atkTimer -= Time.deltaTime;
        float d = dPos.x * dPos.x + dPos.y * dPos.y;
        if (d < avoidDistance * avoidDistance)
            pysc.AddForce(Vector2.ClampMagnitude(dPos.normalized * walkSpeed * pysc.mass, maxImpulse), ForceMode2D.Impulse);
        else
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
        if (atkTimer <= 0f)
            fire(CharCtrl.script.transform);
        if (d < range * range && atkTimer > atkTime)
            atkTimer = atkTime;
    }
}
