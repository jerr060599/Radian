using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadioGroup : MonoBehaviour
{
	List<Activatable> list = new List<Activatable> ();

	public void register (Activatable a)
	{
		list.Add (a);
	}

	public void checkout (Activatable a, CharCtrl player)
	{
		foreach (Activatable b in list)
			if (b.activated && b != a)
				b.activate (player);
	}
}
