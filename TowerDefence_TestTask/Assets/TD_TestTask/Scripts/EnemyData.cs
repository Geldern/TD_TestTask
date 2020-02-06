using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "TD_TestTask/Enemy")]
public class EnemyData : ScriptableObject

{
    public int Health;
    public int Speed;
    public int Damage;
    public int MinKillingReward;
    public int MaxKillingReward;
}
