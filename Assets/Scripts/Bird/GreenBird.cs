using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 scale= new Vector3(-2, 0, 0);
        transform.localScale += scale;
        Vector3 speed = rb.velocity;
        speed.x *= -1;
        rb.velocity = speed;
    }
}
