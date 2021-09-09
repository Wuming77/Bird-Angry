using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    /// <summary>
    /// 重写炫技的函数
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        rb.velocity *= 2;
    }
}
