using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string scene;
    public GameObject diff, menu;
    public void onctinue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
    }
    public void startEpic()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Diff", 0.5f);
        SceneManager.LoadScene(scene);
    }
    public void startLegend()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Diff", 1f);
        SceneManager.LoadScene(scene);
    }
    public void back()
    {
        menu.SetActive(true);
        diff.SetActive(false);
    }
    public void diffSel()
    {
        menu.SetActive(false);
        diff.SetActive(true);
    }
}
