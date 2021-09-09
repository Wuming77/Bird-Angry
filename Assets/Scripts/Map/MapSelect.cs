using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public int starNum = 0;
    public bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;


    private void Start()
    {
        if (PlayerPrefs.GetInt("totalNum", 0) >= starNum)
        {
            isSelect = true;
        }

        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);

            //TODo:text显示
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

}
