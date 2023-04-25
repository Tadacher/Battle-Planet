using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// when applied, increases player shield regen by value. shield regen if its zero
/// </summary>
public class PlayerShieldRegen : UpgradeBtnScript
{
    [SerializeField]
    int  minval = 2, maxval = 6;
    int shieldIncr;

    private void Start()
    {
        shieldIncr = Random.Range(minval, maxval);
        gradeName = "Poverful shield generators (Increase ship shield regen by " + shieldIncr + ". Grants 5 shield if there is no shield at all yet";

        InitializeSelf();
    }

    protected override void Effect()
    {
        playerBehavoiur.shieldregen += (short)shieldIncr;
        if (playerBehavoiur.shield == 0) playerBehavoiur.shield += 5;
    }
}
