using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PreEnding : Activatable
{
    public GameObject tablet, boss;
    public float timeToIpad = 10f;
    public override void init()
    {
    }
    public override void activate(CharCtrl player)
    {
        SceneManager.LoadScene("ending");
    }
}
