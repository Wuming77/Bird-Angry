using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;

    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// 点击暂停按钮
    /// </summary>
    public void Pause()
    {
        //1、播放暂停动画
        anim.SetBool("isPause", true);
        anim.SetBool("isResume", false);
        button.SetActive(false);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 点击继续游戏
    /// </summary>
    public void Resume()
    {
        //1、播放动画
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
        //button.SetActive(true);
    }

    /// <summary>
    /// 暂停动画播放完
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 继续游戏动画播放完
    /// </summary>
    public void ResumeAnimEnd()
    {
        anim.SetBool("isResume", true);
        button.SetActive(true);
    }
}
