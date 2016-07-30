using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerSpawn : Activatable
{
    public Vector2 spawnOffset = Vector2.zero;
    public override void activate(CharCtrl player)
    {
        if (nextActivatable != null)
            nextActivatable.activate(player);
        PlayerPrefs.SetFloat("spawnX_" + SceneManager.GetActiveScene().name, transform.position.x + spawnOffset.x);
        PlayerPrefs.SetFloat("spawnY_" + SceneManager.GetActiveScene().name, transform.position.y + spawnOffset.y);
    }
    public override void init()
    {
    }
}
