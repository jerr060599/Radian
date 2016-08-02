using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Waypoint : MonoBehaviour
{
    public readonly int USED = 0, INACTIVE = 1, ACTIVE = 2;
    public bool activeOnFirstLoad = false;
    public GameObject nextWaypoint = null;
    public long posHash = 0;
    void Start()
    {
        posHash = ((long)(transform.position.x) << 32) + (long)(transform.position.y);
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_waypoint_" + posHash) && activeOnFirstLoad)
            activate();
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_waypoint_" + posHash, INACTIVE) == USED)
            Destroy(gameObject);
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_waypoint_" + posHash, INACTIVE) == INACTIVE)
            gameObject.SetActive(false);
    }

    public virtual void activate()
    {
        gameObject.SetActive(true);
        CustomCursor.script.curWaypoint = this;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_waypoint_" + posHash, ACTIVE);
    }

    public virtual void use()
    {
        if (nextWaypoint)
            nextWaypoint.GetComponent<Waypoint>().activate();
        else
            CustomCursor.script.curWaypoint = null;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_waypoint_" + posHash, USED);
        Destroy(gameObject);
    }
}
