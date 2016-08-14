using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(RawImage))]
public class VideoPlayer : MonoBehaviour
{
    public MovieTexture mov;
    public string lvl;
    void Start()
    {
        mov.filterMode = FilterMode.Point;
        GetComponent<RawImage>().texture = mov as MovieTexture;
        GetComponent<RawImage>().texture.filterMode = FilterMode.Point;
        GetComponent<AudioSource>().clip = mov.audioClip;
        mov.Play();
        GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        if (!mov.isPlaying)
        {
            SceneManager.LoadScene(lvl);
            Destroy(this);
        }
    }
}
