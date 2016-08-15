using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string scene;
    public GameObject diff, menu;
    public void onctinue()
    {
        if (PlayerPrefs.HasKey("lastScene"))
        {
            GetComponent<AudioSource>().Play();
            SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
        }
        else
        {
            GetComponent<AudioSource>().Play();
            menu.SetActive(false);
            diff.SetActive(true);
        }
    }
    public void startEpic()
    {
        GetComponent<AudioSource>().Play();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Diff", 0.5f);

        SceneManager.LoadScene(scene);
    }
    public void startLegend()
    {
        GetComponent<AudioSource>().Play();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Diff", 1f);
        SceneManager.LoadScene(scene);
    }
    public void back()
    {
        GetComponent<AudioSource>().Play();
        menu.SetActive(true);
        diff.SetActive(false);
    }
    public void diffSel()
    {
        GetComponent<AudioSource>().Play();
        menu.SetActive(false);
        diff.SetActive(true);
    }
}
