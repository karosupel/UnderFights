using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAttack : IAttack
{
    RatAttacksScript ratAttacksScript;
    float spinningSpeed;
    int spins;
    float angleWhenColumnSpawns;

    public SpinningAttack(RatAttacksScript ratAttacksScript, float spinningSpeed, int spins, float angleWhenColumnSpawns)
    {
        this.ratAttacksScript = ratAttacksScript;
        this.spinningSpeed = spinningSpeed;
        this.spins = spins;
        this.angleWhenColumnSpawns = angleWhenColumnSpawns;
    }

    public IEnumerator Execute()
    {
        yield return ratAttacksScript.SpinningAttack(spinningSpeed, spins, angleWhenColumnSpawns);
    }
}
