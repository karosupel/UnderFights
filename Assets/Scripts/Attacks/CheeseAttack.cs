using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseAttack : IAttack
{
    RatAttacksScript ratAttacksScript;
    float speed;
    float lifetime;

    public CheeseAttack(RatAttacksScript ratAttacksScript, float speed, float lifetime)
    {
        this.ratAttacksScript = ratAttacksScript;
        this.speed = speed;
        this.lifetime = lifetime;
    }

    public IEnumerator Execute()
    {
        yield return ratAttacksScript.CheeseAttack(speed, lifetime);
    }
}
