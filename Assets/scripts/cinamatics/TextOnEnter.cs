using UnityEngine;
using System.Collections;

public class TextOnEnter : MonoBehaviour
{
    public GameObject canvas = null, defText;
    public float smooth = 0.6f;
    public string content = "";
    RectTransform rt = null;
    GameObject txt = null;
    UnityEngine.UI.Text txtComp = null;
    public Vector2 offset = Vector2.zero;
    bool show = false;
    void Start()
    {
        txt = Instantiate(defText);
        txt.transform.SetParent(canvas.transform);
        txt.GetComponent<RectTransform>().localPosition = transform.position + (Vector3)offset;
        txtComp = txt.GetComponent<UnityEngine.UI.Text>();
        txtComp.text = content;
        txtComp.color = new Color(txtComp.color.r, txtComp.color.g, txtComp.color.b, 0f);
        txt.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        show = true;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        show = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (show)
            txtComp.color = new Color(txtComp.color.r, txtComp.color.g, txtComp.color.b, txtComp.color.a + (1 - txtComp.color.a) * (1 - smooth));
        else
            txtComp.color = new Color(txtComp.color.r, txtComp.color.g, txtComp.color.b, txtComp.color.a * smooth);
        if (txtComp.color.a > 0.05)
        {
            if (!txt.activeSelf)
                txt.SetActive(true);
        }
        else if (txt.activeSelf)
            txt.SetActive(false);
    }
}
