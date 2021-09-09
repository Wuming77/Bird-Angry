using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    private List<pig> blocks = new List<pig>();
    public AudioClip boomClip;

    /// <summary>
    /// 进入触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<pig>());
        }
    }

    /// <summary>
    /// 离开触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<pig>());
        }
    }

    /// <summary>
    /// 重写炫技方法
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blocks.Count > 0 && blocks != null)
        {
            for(int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    void OnClear()
    {
        rb.velocity = Vector3.zero;
        AudioPlay(boomClip);
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }

    /// <summary>
    /// 重写下一只小鸟的方法
    /// </summary>
    protected override void Next()
    {
        GameManager._Instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._Instance.NextBird();
    }


}
