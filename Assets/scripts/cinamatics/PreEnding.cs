using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PreEnding : Activatable
{
    public GameObject tablet, boss;
    public override void init()
    {
    }
    public override void activate(CharCtrl player)
    {
        tablet.transform.position = boss.transform.position;
        tablet.SetActive(true);
    }
}
