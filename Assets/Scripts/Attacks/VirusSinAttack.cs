using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSinAttack : IAttack
{
    RatAttacksScript ratAttacksScript;
    int waves;
    float waveFrequency;

    public VirusSinAttack(RatAttacksScript ratAttacksScript, int waves, float waveFrequency)
    {
        this.ratAttacksScript = ratAttacksScript;
        this.waves = waves;
        this.waveFrequency = waveFrequency;
    }

    public IEnumerator Execute()
    {
        yield return ratAttacksScript.VirusSinAttack(waves, waveFrequency);
    }
}
