using UnityEngine;
using System.Collections;

public class DeathRegistry : MonoBehaviour
{
    public GameObject activatable = null;
    public int deathRequired = 1;
    public void registerDeath()
    {
        deathRequired--;
        if (deathRequired <= 0 && activatable)
            if (activatable.GetComponent<Activatable>())
                activatable.GetComponent<Activatable>().activate(CharCtrl.script);
    }
}
