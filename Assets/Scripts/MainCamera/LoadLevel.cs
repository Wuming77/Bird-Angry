using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
    }
}
