using UnityEngine;
using System.Collections;

public class SeekerAi : BasicEnemy
{
    public float agroRadius = 2f, parkRadius = 1.1f, maxImpulse = 1f;
	bool agroStay=false;
	float timer=1.5f;
	float accumulator;
	float deathTimer=2;
	bool die=false;
	Vector2 dPos;
	void Start()
	{
		pysc = GetComponent<Rigidbody2D> ();
		accumulator = timer;

	}
    // Update is called once per frame
    protected void Update()
    {
		dPos = CharCtrl.script.pysc.position - pysc.position;
		if (die) {
			deathTimer -= Time.deltaTime;

		}
		if (deathTimer < 0.01f)
			Destroy (gameObject);
		//pysc.position += dashPos * dashLerp;
		//dashPos *= 1 - dashLerp;
        agro = (CharCtrl.script.pysc.position - pysc.position).sqrMagnitude <= agroRadius * agroRadius;
		if (agro || agroStay)
        {
			agroStay = true;
        
			if (dPos.sqrMagnitude > parkRadius * parkRadius) {
				if (!die) {
				pysc.AddForce (Vector2.ClampMagnitude (((CharCtrl.script.pysc.position - pysc.position).normalized * walkSpeed - pysc.velocity) * pysc.mass, maxImpulse), ForceMode2D.Impulse);

					if (dPos.x <= 0)
						GetComponent<Animator> ().Play ("EnemyWalk");
					else
						GetComponent<Animator> ().Play ("EnemyWalkFlipped");
				}
				else
				pysc.AddForce (Vector2.ClampMagnitude (-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
			} else {
				pysc.AddForce (Vector2.ClampMagnitude (-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
				if (dPos.x <= 0) {

					accumulator -= Time.deltaTime;

					if (accumulator <= 0.01f) {
						CharCtrl.script.damage (0.10f);
						accumulator = timer;
						GetComponent<Animator> ().Play ("EnemyMelee");

					}
				} else {
					accumulator -= Time.deltaTime;

					if (accumulator <= 0.01f) {
						CharCtrl.script.damage (0.10f);
						accumulator = timer;
						GetComponent<Animator> ().Play ("EnemyMeleeFlipped");
					}


				}
			}
	
        }
        else
            pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxImpulse), ForceMode2D.Impulse);
    }

	public override void kill(int damageType = 0)
    {
		if (damageType == MELEE_DAMAGE) {
			if (dPos.x <= 0)
			GetComponent<Animator> ().Play ("EnemyMeleeDeath");
			else
				GetComponent<Animator> ().Play ("EnemyMeleeDeathFlipped");
				
		} else if (damageType == RANGED_DAMAGE) {
			if (dPos.x <= 0)
			GetComponent<Animator> ().Play ("EnemyArrowDeath");
			else
				GetComponent<Animator> ().Play ("EnemyArrowDeathFlipped");
		}
		die = true;
    }

	public override void damage(int amount,int damageType = 0)
	{
		base.damage (amount, damageType);
		if (!die) {
			if (dPos.x <= 0)
				GetComponent<Animator> ().Play ("EnemyStagger");
			else
				GetComponent<Animator> ().Play ("EnemyStaggerFlipped");
		}

	}
}
