using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when applied, increases planet hp by value
/// </summary>
public class IncreasePlanetHp : UpgradeBtnScript
{
    
    [SerializeField]
    int minval = 15, maxval=26;
    int hpIncrease;

    private void Start()
    {
        //initialize upgrade after being constructed;
       
        hpIncrease = Random.Range(minval, maxval);
        gradeName = "Upgrade planetary defence forces (Increase planetary hp by " + hpIncrease;
        InitializeSelf();
    }

    protected override void Effect()
    {
        planetHitpoints._maxHp += (short)hpIncrease;
        planetHitpoints.RecieveDamage(-1 * hpIncrease);
    }
}
