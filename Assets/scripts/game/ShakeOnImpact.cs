using UnityEngine;
using System.Collections;

public class ShakeOnImpact : OnImpact
{
	public override void impact ()
	{
		CameraMovement.script.shake ();
	}
}
