using UnityEngine;
using System.Collections;

public class TimeRift : MonoBehaviour
{
    public float timeScale = 1f, duration;
    public bool fixedDuration = true, seppukuObject = true;
    public int validUsage = 1;
    float prevTimeScale = 0f;
    float riftTimer = float.PositiveInfinity;
    void Update()
    {
        if (fixedDuration)
            riftTimer -= Time.unscaledDeltaTime;
        if (riftTimer <= 0f)
        {
            Time.timeScale = prevTimeScale;
            riftTimer = float.PositiveInfinity;
            if (validUsage <= 0)
                if (seppukuObject)
                    Destroy(gameObject);
                else
                    Destroy(this);
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject && riftTimer > duration && validUsage > 0)
        {
            validUsage--;
            prevTimeScale = Time.timeScale;
            Time.timeScale = timeScale;
            riftTimer = duration;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject && !fixedDuration)
        {
            Time.timeScale = prevTimeScale;
            riftTimer = float.PositiveInfinity;
            if (validUsage <= 0)
                Destroy(gameObject);
        }
    }
}
