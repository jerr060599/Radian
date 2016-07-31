using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Waypoint : MonoBehaviour
{
    public readonly int USED = 0, INACTIVE = 1, ACTIVE = 2;
    public bool activeOnFirstLoad = false;
    public GameObject nextWaypoint = null;
    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_waypoint_" + GetInstanceID()) && activeOnFirstLoad)
            activate();
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_waypoint_" + GetInstanceID(), INACTIVE) == USED)
            Destroy(gameObject);
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_waypoint_" + GetInstanceID(), INACTIVE) == INACTIVE)
            gameObject.SetActive(false);

    }

    public virtual void activate()
    {
        gameObject.SetActive(true);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_waypoint_" + GetInstanceID(), ACTIVE);
    }

    public virtual void use()
    {
        if (nextWaypoint)
            nextWaypoint.GetComponent<Waypoint>().activate();
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_waypoint_" + GetInstanceID(), USED);
        Destroy(gameObject);
    }
}
