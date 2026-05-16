using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightFoodAttack : IAttack
{
    PurinAttacksScript purin;
    int amount_of_food;
    float offset_between_food;

    int number_of_repetitions;

    float time_between_repetitions;
    float time_between_food_spawn;

    public StraightFoodAttack(PurinAttacksScript purin)
    {
        this.purin = purin;
        this.amount_of_food = purin.amount_of_food;
        this.offset_between_food = purin.offset_between_food;
        this.number_of_repetitions = purin.number_of_repetitions;
        this.time_between_repetitions = purin.time_between_repetitions;
        this.time_between_food_spawn = purin.time_between_food_spawn;
    }

    public IEnumerator Execute()
    {
        yield return purin.StraightFoodAttack(amount_of_food, offset_between_food, number_of_repetitions, time_between_repetitions, time_between_food_spawn);
    }
}
