using UnityEngine;
using System.Collections;

public abstract class Activatable : MonoBehaviour
{
    public bool activated = false, playerActivatable = true;
    public RadioGroup radioGroup = null;
    public Sprite activatedTex, deactivatedTex;
    public Activatable chainedActivatable = null;
    protected SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (radioGroup != null)
            radioGroup.register(this);
        init();
    }

    public virtual void init()
    {
        if (activated)
            sr.sprite = activatedTex;
        else
            sr.sprite = deactivatedTex;
    }

    public virtual void activate(CharCtrl player)
    {
        if (activated)
        {
            sr.sprite = deactivatedTex;
            activated = false;
            onDeactivation(player);
        }
        else
        {
            sr.sprite = activatedTex;
            activated = true;
            onActivation(player);
            if (radioGroup != null)
                radioGroup.checkout(this, player);
        }
        if (chainedActivatable != null)
            chainedActivatable.activate(player);
    }

    public virtual void onActivation(CharCtrl player)
    {
    }

    public virtual void onDeactivation(CharCtrl player)
    {
    }
}
