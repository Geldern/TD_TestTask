using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "TD_TestTask/Tower")]
public class TowerData : ScriptableObject
{
    public int BuildPrice;
    public int SellPrice;
    public int Damage;
    public float ShootInterval;
    public float Range;
}
