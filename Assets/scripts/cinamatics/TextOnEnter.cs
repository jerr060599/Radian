using UnityEngine;
using System.Collections;

public class TextOnEnter : MonoBehaviour
{
    public GameObject canvas = null, defText;
    public float smooth = 0.6f;
    public string content = "";
    GameObject txt = null;
    UnityEngine.UI.Text txtComp = null;
    public Vector2 offset = Vector2.zero;
    bool show = false;
    public bool once;
    bool onceT;
    Color curC = new Color(1, 1, 1, 0);
    void Start()
    {
        txt = Instantiate(defText);
        txt.transform.SetParent(canvas.transform);
        txt.GetComponent<RectTransform>().position = transform.position + (Vector3)offset;
        txtComp = txt.GetComponent<UnityEngine.UI.Text>();
        txtComp.text = content;
        txtComp.color = curC;
        txt.SetActive(false);
        onceT = false;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
            show = true;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == CharCtrl.script.gameObject)
            show = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (show)
        {
            curC.a += (1 - curC.a) * smooth;
            txtComp.color = curC;
        }
        else
        {
            curC.a -= curC.a * smooth;
            txtComp.color = curC;
        }
        if (txtComp.color.a > 0.05)
        {
            if (!txt.activeSelf)
            {
                if (!onceT)
                    txt.SetActive(true);
            }
        }
        else if (txt.activeSelf)
        {
            txt.SetActive(false);
            if (once)
                onceT = true;
        }
    }
}
