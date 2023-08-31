using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when applied, increases player regen by value
/// </summary>
public class PlayerHpRegen : UpgradeBtnScript
{  
    [SerializeField]
    int  maxval = 1, minval = 4;
    int regenIncrease;
    private void Start()
    {
        //initialize upgrade after being constructed;
        regenIncrease = Random.Range(minval, maxval);
        gradeName = "Battlestation repair droids (Increase player regen by " + regenIncrease + "/sec";

        InitializeSelf();
    }
    protected override void Effect()
    {
        playerBehavoiur.hpRegen += (short)regenIncrease;
    }
}
