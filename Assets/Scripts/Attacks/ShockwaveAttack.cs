using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveAttack : IAttack
{
    PurinAttacksScript purin;
    int amount;
    float delay;

    public ShockwaveAttack(PurinAttacksScript purin, int amount, float delay)
    {
        this.purin = purin;
        this.amount = amount;
        this.delay = delay;
    }

    public IEnumerator Execute()
    {
        MainManagerScript.Instance.boxScript.Resize(MainManagerScript.Instance.f_panel_position, new Vector2(1.2f, 0.7f));
        yield return purin.SchockwaveAttack(amount, delay);
    }
}
