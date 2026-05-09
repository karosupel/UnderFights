using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    public float health;
    public float attack;
    public float defense;
}

