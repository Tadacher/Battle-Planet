using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when applied, increases plsnet regen by value
/// </summary>
public class PlanetaryRegenIncrease : UpgradeBtnScript
{
    [SerializeField]
    int  minval = 1, maxval = 4;
    int regenIncr;

    private void Start()
    {
        //initialize upgrade after being constructed;
        regenIncr = Random.Range(minval, maxval);

        gradeName = "Counter-invasive terraformation (Increase planetary regen by " + regenIncr + "/sec";
        InitializeSelf();
    }

    protected override void Effect()
    {
        planetHitpoints.hpRegen += (short)regenIncr;
    }
}
