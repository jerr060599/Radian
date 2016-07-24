using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public GameObject defSrc, bgmSrc;
    public static SoundManager script;
    public AudioClip jump0, jump1, jump2, click0, death0, punch0, climb0, climb1, walk0, walk1, pickup0, checkpoint0, explosion0, win0;
    public AudioSource lastBGM = null, curBGM = null;
    public float smooth = 0.1f;
    public float bgmVolume = 0f;
    void Start()
    {
        script = this;
    }
    public AudioSource playOnListener(AudioClip clip, float volume = 1f)
    {
        GameObject src = (GameObject)Instantiate(defSrc, transform.position, transform.rotation);
        src.transform.SetParent(gameObject.transform);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = volume;
        src.GetComponent<AudioSource>().Play();
        return src.GetComponent<AudioSource>();
    }

    public AudioSource playOn(Vector2 pos, AudioClip clip, float volume = 1f)
    {
        GameObject src = (GameObject)Instantiate(defSrc, pos, Quaternion.identity);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = volume;
        src.GetComponent<AudioSource>().Play();
        return src.GetComponent<AudioSource>();
    }

    public AudioSource playBGM(AudioClip clip, float volume)
    {
        GameObject src = (GameObject)Instantiate(bgmSrc, transform.position, transform.rotation);
        src.transform.SetParent(gameObject.transform);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = 0f;
        bgmVolume = volume;
        src.GetComponent<AudioSource>().Play();
        if (lastBGM)
            Destroy(lastBGM);
        lastBGM = curBGM;
        return curBGM = src.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (curBGM)
            curBGM.volume += (bgmVolume - curBGM.volume) * smooth;
        if (lastBGM)
        {
            lastBGM.volume = Mathf.Max(lastBGM.volume * smooth - 0.01f, 0f);
            if (lastBGM.volume == 0f)
                Destroy(lastBGM.gameObject);
        }
    }
}
