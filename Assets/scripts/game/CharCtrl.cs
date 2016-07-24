using UnityEngine;
using System.Collections;

public class CharCtrl : MonoBehaviour
{
    public static CharCtrl script = null;
    public GameObject death, itemIcon, spawn, lightBar, darkBar, gemObject;
    public bool controllable = true, usingLight = true, isDashing = false;
    public float charSpeed = 10f, maxBrakeF = 3f, dashDist = 2f, dashCoolDown = 1f, dashLerp = 0.1f, meleeRadius = 2f, meleeField = 0f, meleeCoolDown = 0.5f;
    public float meleeCost = 0.05f, dashCost = 0.01f;
    public float darkMultiplyer = 2f;
    public int meleeDamage = 1;
    public int dashLayer = 9;
    public Rigidbody2D pysc = null;
    public BarCtrl light, dark;
    public GemCtrl gem;
    float dashTime = 0f, meleeTime = 0f;
    Vector2 lastInput = Vector2.down;
    Animator ani;
    SpriteRenderer sr;
    Consumable item;
    Vector2 lastJuicePos, dashPos;
    CircleCollider2D cc;
    // Use this for initialization
    void Start()
    {
        light = lightBar.GetComponent<BarCtrl>();
        dark = darkBar.GetComponent<BarCtrl>();
        Cursor.SetCursor(Settings.defCursor, Settings.defCursorCenter, CursorMode.Auto);
        script = this;
        pysc = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lastJuicePos = pysc.position;
        cc = GetComponent<CircleCollider2D>();
        gem = gemObject.GetComponent<GemCtrl>();
        ani = GetComponent<Animator>();
    }
    public void damage(float amount)
    {
        light.barPercent -= amount;
    }
    public void kill()
    {
        //((GameObject)Instantiate(death, transform.position + (new Vector3(0f, 12f, 0f)), transform.rotation)).GetComponent<RespawnAni>().player = this;
        //gameObject.SetActive(false);
        //SoundManager.script.playOnListener (SoundManager.script.death0, 0.4f);
        respawn();
    }

    public void respawn()
    {
        //gameObject.SetActive(true);
        //pysc.velocity = Vector2.zero;
        //transform.position = new Vector3(curSpawn.transform.position.x, curSpawn.transform.position.y - 4f, 0f);
        light.barPercent = 1f;
        dark.barPercent = 0f;
        transform.position = spawn.transform.position;
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
        if (light.barPercent <= 0f)
        {
            kill();
            return;
        }
        dashTime -= Time.deltaTime;
        meleeTime -= Time.deltaTime;
        Vector2 redirect = Vector2.right;
        Vector2 center = pysc.position + cc.offset;
        if (isDashing = dashPos.sqrMagnitude > 0.1f)
            gameObject.layer = dashLayer;
        else
            gameObject.layer = 0;
        foreach (RaycastHit2D rh in Physics2D.CircleCastAll(center, 0.5f, Vector2.down, 0f))
            if (rh.collider.isTrigger)
                if (!isDashing && rh.collider.gameObject.GetComponent<Air>())
                {
                    //controllable = false;
                    kill();
                    break;
                }
                else if (rh.collider.gameObject.GetComponent<MovementRedirect>())
                    redirect = rh.collider.gameObject.GetComponent<MovementRedirect>().dir;
        if (controllable)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (!isDashing)
            {
                if (input.sqrMagnitude != 0f)
                {
                    lastInput = input;
                    input = input.normalized;
                    input = redirect * input.x + new Vector2(-redirect.y, redirect.x) * input.y;
                    pysc.AddForce((input * charSpeed - pysc.velocity) * pysc.mass, ForceMode2D.Impulse);
                    if (Mathf.Abs(input.x) >= Mathf.Abs(input.y))
                        if (input.x > 0)
                            ani.Play("RightWalk", 0);
                        else
                            ani.Play("LeftWalk", 0);
                    else if (input.y > 0)
                        ani.Play("UpWalk", 0);
                    else
                        ani.Play("DownWalk", 0);
                }
                else
                {
                    pysc.AddForce(Vector2.ClampMagnitude(-pysc.velocity * pysc.mass, maxBrakeF), ForceMode2D.Impulse);
                    if (Mathf.Abs(lastInput.x) >= Mathf.Abs(lastInput.y))
                        if (lastInput.x > 0)
                            ani.Play("idleRight", 0);
                        else
                            ani.Play("idleLeft", 0);
                    else if (lastInput.y > 0)
                        ani.Play("idleUp", 0);
                    else
                        ani.Play("idleDown", 0);
                }
            }
            else if (Mathf.Abs(dashPos.x) > Mathf.Abs(dashPos.y))
                if (dashPos.x > 0)
                    ani.Play("RightDash", 0);
                else
                    ani.Play("LeftDash", 0);
            else if (dashPos.y > 0)
                ani.Play("UpDash", 0);
            else
                ani.Play("DownDash", 0);
            Vector2 rPos = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)) - center).normalized;
            if (dashTime <= 0f && Input.GetKeyDown(Settings.keys[Settings.player, Settings.dash]))
            {
                float closest = dashDist;
                foreach (RaycastHit2D rh in Physics2D.RaycastAll(center, rPos, dashDist))
                    if (!rh.collider.isTrigger && rh.distance < closest && !(rh.collider.attachedRigidbody && rh.collider.attachedRigidbody.gameObject == gameObject))
                        closest = rh.distance;
                dashPos = rPos * closest;
                dashTime = dashCoolDown;
                cost(dashCost);
            }
            if (meleeTime <= 0f && Input.GetMouseButtonDown(0))
            {
                BasicEnemy be = null;
                foreach (RaycastHit2D rh in Physics2D.CircleCastAll(pysc.position, meleeRadius, Vector2.down, 0f))
                    if (!rh.collider.isTrigger && (be = rh.collider.gameObject.GetComponent<BasicEnemy>()) && Vector2.Dot((rh.point - pysc.position).normalized, rPos) >= meleeField)
                        be.damage(meleeDamage, BasicEnemy.MELEE_DAMAGE);
                meleeTime = meleeCoolDown;
                cost(meleeCost);
            }
            if (Input.GetKeyDown(Settings.keys[Settings.player, Settings.toggleEnergy]))
            {
                usingLight = !usingLight;
                gem.isLight = usingLight;
            }
        }
        else
            ani.Play("idleDown", 0);
        pysc.position += dashPos * dashLerp;
        dashPos *= 1 - dashLerp;
    }
    public void cost(float cost)
    {
        if (usingLight)
            light.barPercent -= cost;
        else
            dark.barPercent += cost / darkMultiplyer;
    }
}
