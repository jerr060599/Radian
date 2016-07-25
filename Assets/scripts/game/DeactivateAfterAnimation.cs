using UnityEngine;
using System.Collections;

public class DeactivateAfterAnimation : MonoBehaviour
{

    Animator ani;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void play(string animation)
    {
        gameObject.SetActive(true);
        ani.Play(animation);
    }
    // Update is called once per frame
    void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !ani.IsInTransition(0))
            gameObject.SetActive(false);
    }
}
