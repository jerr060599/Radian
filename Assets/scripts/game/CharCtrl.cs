using UnityEngine;
using System.Collections;

public class CharCtrl : MonoBehaviour
{
    public static CharCtrl script = null;
    public GameObject death, itemIcon, baseSpawn, lightBar, darkBar;
    public bool controllable = true, usingLight = true;
    public float charSpeed = 10f, maxBrakeF = 3f, dashDist = 2f, dashCoolDown = 1f, dashLerp = 0.1f, meleeRadius = 2f, meleeField = 0f, meleeCoolDown = 0.5f;
    public float meleeCost = 0.05f, dashCost = 0.01f;
    public float darkMultiplyer = 2f;
    public int meleeDamage = 1;
    public Rigidbody2D pysc = null;
    public BarCtrl light, dark;
    float dashTime = 0f, meleeTime = 0f;
    SpriteRenderer sr;
    GameObject curSpawn;
    Consumable item;
    Vector2 lastJuicePos, dashPos;
    // Use this for initialization
    void Start()
    {
        light = lightBar.GetComponent<BarCtrl>();
        dark = darkBar.GetComponent<BarCtrl>();
        Cursor.SetCursor(Settings.defCursor, Settings.defCursorCenter, CursorMode.Auto);
        script = this;
        pysc = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        curSpawn = baseSpawn;
        lastJuicePos = pysc.position;
    }
    public void kill()
    {
        //((GameObject)Instantiate(death, transform.position + (new Vector3(0f, 12f, 0f)), transform.rotation)).GetComponent<RespawnAni>().player = this;
        //gameObject.SetActive(false);
        //SoundManager.script.playOnListener (SoundManager.script.death0, 0.4f);
    }

    public void respawn()
    {
        //gameObject.SetActive(true);
        //pysc.velocity = Vector2.zero;
        //transform.position = new Vector3(curSpawn.transform.position.x, curSpawn.transform.position.y - 4f, 0f);
    }

    public bool eat(Consumable c)
    {
        if (item == null)
        {
            item = c;
            itemIcon.GetComponent<UnityEngine.UI.Image>().sprite = c.icon;
            SoundManager.script.playOnListener(SoundManager.script.pickup0, 0.8f);
            itemIcon.SetActive(true);
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        dashTime -= Time.deltaTime;
        meleeTime -= Time.deltaTime;
        Vector2 redirect = Vector2.right;
        foreach (RaycastHit2D rh in Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.down, 0f))
            if (rh.collider.isTrigger)
                if (rh.collider.gameObject.GetComponent<Air>())
                {
                    controllable = false;
                    break;
                }
                else if (rh.collider.gameObject.GetComponent<MovementRedirect>())
                    redirect = rh.collider.gameObject.GetComponent<MovementRedirect>().dir;
        if (controllable)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (input.sqrMagnitude != 0f)
            {
                input = input.normalized;
                input = redirect * input.x + new Vector2(-redirect.y, redirect.x) * input.y;
                pysc.AddForce((input * charSpeed - pysc.velocity) * pysc.mass, ForceMode2D.Impulse);
            }
            else
                pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxBrakeF), ForceMode2D.Impulse);
            Vector2 rPos = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)) - pysc.position).normalized;
            if (dashTime <= 0f && Input.GetKeyDown(Settings.keys[Settings.player, Settings.dash]))
            {
                float closest = dashDist;
                foreach (RaycastHit2D rh in Physics2D.RaycastAll(pysc.position, rPos, dashDist))
                    if (!rh.collider.isTrigger && rh.distance < closest && !(rh.collider.attachedRigidbody.gameObject && rh.collider.attachedRigidbody.gameObject == gameObject))
                        closest = rh.distance;
                dashPos = rPos * closest;
                dashTime = dashCoolDown;
                cost(dashCost);
            }
            if (meleeTime <= 0f && Input.GetMouseButtonDown(0))
            {
                BasicEnemy be = null;
                foreach (RaycastHit2D rh in Physics2D.CircleCastAll(transform.position, meleeRadius, Vector2.down, 0f))
                    if (!rh.collider.isTrigger && (be = rh.collider.gameObject.GetComponent<BasicEnemy>()) && Vector2.Dot((rh.point - pysc.position).normalized, rPos) >= meleeField)
                        be.damage(meleeDamage);
                meleeTime = meleeCoolDown;
                cost(meleeCost);
            }
        }
        pysc.position += dashPos * dashLerp;
        dashPos *= 1 - dashLerp;
    }
    public void cost(float cost)
    {
        if (usingLight)
            light.barPercent -= cost;
        else
            dark.barPercent += +cost;
    }
}
