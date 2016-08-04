using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    CanvasRenderer canvas;
    float alpha = 0f, lastVolume;
    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;
        canvas = GetComponent<CanvasRenderer>();
        canvas.SetAlpha(alpha);
        Time.timeScale = 1f;
        //paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
            alpha = paused ? 0.9f : 0f;
            lastVolume = SoundManager.script.bgmVolume;
            SoundManager.script.bgmVolume = paused ? 0f : lastVolume;
        }
        if (Input.GetKeyDown(KeyCode.M) && paused)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("mainMenu");
        }
        canvas.SetAlpha(canvas.GetAlpha() + (alpha - canvas.GetAlpha()) * 0.1f);
        if (canvas.GetAlpha() < 0.5f)
            GetComponent<Canvas>().enabled = false;
        else
            GetComponent<Canvas>().enabled = true;
    }
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
