using UnityEngine;
using System.Collections;

public class RollSetter : MonoBehaviour {

	public float w = 0f;

	public void OnTriggerEnter2D (Collider2D c)
	{
		ConstantRoll cr;
		if ((cr = c.gameObject.GetComponent<ConstantRoll> ()) != null)
			cr.w = w;
	}
}

