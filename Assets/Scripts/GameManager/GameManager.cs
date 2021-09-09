using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<pig> pigs;

    public static GameManager _Instance;

    private Vector3 originPos;//初始化每一次使用的小鸟的位置

    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;

    private int starsNum = 0;//存储玩家所获得的星星数

    private void Awake()
    {
        _Instance = this;
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }
    /// <summary>
    /// 初始化小鸟
    /// </summary>
    private void Initialized()
    {
        for (int i=0;i<birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    /// <summary>
    /// 判定游戏逻辑
    /// </summary>
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只飞吧
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }
    public void ShowStars()
    {
        StartCoroutine("Show");
    }
    IEnumerator Show()
    {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.6f);
            stars[starsNum].SetActive(true);
        }
    }

    /// <summary>
    /// 再次开始当前关卡
    /// </summary>
    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
    }
}
