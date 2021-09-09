using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    #region private

    private bool isClick = false;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rb;

    #endregion

    #region public

    public LineRenderer right;
    public Transform rightPos;//关节起点
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;


    public float maxDis = 3;//最大距离限制

    public float smooth = 3;//相机跟随平滑度

    protected BirdTrail myTrail;

    private bool canMove = true;

    public AudioClip select;
    public AudioClip fly;

    private bool isFly;

    public Sprite hurt;
    protected SpriteRenderer render;

    #endregion

    #region  小鸟对应的鼠标事件
    private void OnMouseDown() //鼠标按下
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rb.isKinematic = true;
        }
    }

    private void OnMouseUp() //鼠标抬起
    {
        if (canMove)
        {
            isClick = false;
            rb.isKinematic = false;
            Invoke("Fly", 0.1f);
            //禁用划线
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
    }
    #endregion

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<BirdTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isClick) //通过鼠标设置小鸟位置
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                //限制小鸟的位置
                Vector3 pos = (transform.position - rightPos.position).normalized;//得到指向小鸟的单位向量
                pos *= maxDis;//转成最大值
                transform.position = pos + rightPos.position;//小鸟位置
            }
            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Vector3 cameraPos = new Vector3(Mathf.Clamp(posX, -1.27f, 15.5f), Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, smooth * Time.deltaTime);

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isFly = true;
        AudioPlay(fly);
        myTrail.TrailStarts();
        sp.enabled = false;
        Invoke("Next", 5);
    }

    /// <summary>
    /// 划线
    /// </summary>
    void Line()
    {
        //启用划线
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }
    /// <summary>
    /// 下一只小鸟飞出
    /// </summary>
    protected virtual void Next()
    {
        GameManager._Instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._Instance.NextBird();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrails();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 炫技操作
    /// </summary>
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    /// <summary>
    /// 小鸟受伤 
    /// </summary>
    public void Hurt()
    {
        render.sprite = hurt;
    }
}
