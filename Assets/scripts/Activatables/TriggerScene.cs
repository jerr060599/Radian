using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TriggerScene : Activatable
{
    public string scene;
    public override void init() { }
    public override void activate(CharCtrl p)
    {
        SceneManager.LoadScene(scene);
    }
}
