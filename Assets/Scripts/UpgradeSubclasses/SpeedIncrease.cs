using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// when applied, increases player thrust speed by value
/// </summary>
public class SpeedIncrease : UpgradeBtnScript
{
    
    [SerializeField]
    float maxPercentage = 1.15f, minPercentage = 1.05f;
    float percentage;

    private void Start()
    {
        //initialize upgrade after being constructed;
        
        percentage = Random.Range(minPercentage,maxPercentage);
        gradeName = "Additional maneuver thrusters (Increase thrust speed by " + percentage + "%)";
        InitializeSelf();
    }
    protected override void Effect()
    {
        shipcontroll.thrustSpeed *= percentage;       
    }
}
