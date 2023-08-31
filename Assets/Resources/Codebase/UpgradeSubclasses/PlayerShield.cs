using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when applied, increases player shield by value. adds shield regen if its zero
/// </summary>

public class PlayerShield : UpgradeBtnScript
{
    [SerializeField]
    int  minval = 5, maxval = 10;
    int shieldIncr;

    private void Start()
    {
        //initialize upgrade after being constructed;
        shieldIncr = Random.Range(minval, maxval);
        gradeName = "More capacious shield batteries (increase player shield by "+shieldIncr +" pts)";
        InitializeSelf();
    }

    protected override void Effect()
    {
        playerBehavoiur._maxShield += (short)shieldIncr;
        if (playerBehavoiur.shieldregen == 0) playerBehavoiur.shieldregen += 1;
    }
}
