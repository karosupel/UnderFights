using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : IAttack
{
    PurinAttacksScript purin;

    public ExplosionAttack(PurinAttacksScript purin)
    {
        this.purin = purin;
    }

    public IEnumerator Execute()
    {
        yield return purin.ExplosionAttack();
    }
}
