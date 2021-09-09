using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;

    private SpriteRenderer render;
    public Sprite hurt;

    public GameObject boom;
    public GameObject Score;

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

    public bool isPig = false;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.relativeVelocity.magnitude);  查看速度

        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            collision.gameObject.GetComponent<Bird>().Hurt();
        }

        if(collision.relativeVelocity.magnitude>maxSpeed)
        {
            Dead();
        }
        else if(collision.relativeVelocity.magnitude <= maxSpeed&& collision.relativeVelocity.magnitude >= minSpeed)
        {
            render.sprite = hurt;
            AudioPlay(hurtClip);
        }
    }

    public void Dead()
    {
        if (isPig)
        {
            GameManager._Instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        Vector3 position = new Vector3(0, 0.9f, 0) + transform.position;
        GameObject go = Instantiate(Score, position, Quaternion.identity);
        Destroy(go, 1.5f);
        //播放猪死亡的音乐
        AudioPlay(dead);
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
