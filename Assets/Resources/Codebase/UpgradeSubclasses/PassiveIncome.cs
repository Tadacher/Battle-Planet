using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveIncome : UpgradeBtnScript
{
    [SerializeField]
    int  minval = 1, maxval = 4;
    int passiveIncomeIncr;

    private void Start()
    {
        //initialize upgrade after being constructed;
        
        passiveIncomeIncr = Random.Range(minval, maxval);
        gradeName = "Heavy industry cluster (increase lvl-up speed by " + passiveIncomeIncr +" pts/sec";
        InitializeSelf();
    }

    protected override void Effect()
    {
        planetaryUpgrades.passiveIncome += (short)passiveIncomeIncr;
    }
}
