using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerSpawn : Activatable
{
    public Vector2 spawnOffset = Vector2.zero;
    public GameObject effects = null;
    Animator ani = null;
    bool on = false;
    public override void activate(CharCtrl player)
    {
        on = true;
        if (nextActivatable != null)
            nextActivatable.activate(player);
        PlayerPrefs.SetFloat("spawnX_" + SceneManager.GetActiveScene().name, transform.position.x + spawnOffset.x);
        PlayerPrefs.SetFloat("spawnY_" + SceneManager.GetActiveScene().name, transform.position.y + spawnOffset.y);
        PlayerPrefs.SetInt("curSpawn_" + SceneManager.GetActiveScene().name, GetInstanceID());
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("playerSpawn"))
            if (obj != gameObject)
                obj.GetComponent<PlayerSpawn>().turnOff();
        ani.Play("burning", 0);
        effects.SetActive(true);
		if(on)
		GetComponent<AudioSource> ().Play ();
    }
    public void turnOff()
    {
        if (!on)
            return;
        ani.Play("off", 0);
		if(!on)
		GetComponent<AudioSource> ().Stop ();
        on = false;
        effects.SetActive(false);
    }
    public override void init()
    {
        ani = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("curSpawn_" + SceneManager.GetActiveScene().name) == GetInstanceID())
            activate(CharCtrl.script);
    }
}
