using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string scene;

    public void OnClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(scene);
    }
    public void onctinue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
    }
}
