using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour {
    public string scene;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.attachedRigidbody && c.attachedRigidbody.gameObject == CharCtrl.script.gameObject)
            SceneManager.LoadScene(scene);
    }
}
