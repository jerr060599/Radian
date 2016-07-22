using UnityEngine;
using System.Collections;

public class AnimationCollector : MonoBehaviour
{
	Animator ani;
	// Use this for initialization
	void Start ()
	{
		ani = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ani.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !ani.IsInTransition (0))
			Destroy (gameObject);
	}
}
