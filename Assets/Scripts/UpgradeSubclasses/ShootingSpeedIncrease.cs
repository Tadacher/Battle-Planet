using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// multiplies all projectiles shotIntervals by a random value between setted values
/// </summary>
public class ShootingSpeedIncrease : UpgradeBtnScript
{
    [SerializeField]
    float  minVal= 0.03f, maxVal= 0.10f;
    float increasementPercent;
    private void Start()
    {
        increasementPercent = Random.Range(minVal, maxVal);
        gradeName = "Upgrade foton canon refrigerant machinery (Increase ShootingSpeed by " + increasementPercent+"%)";

        InitializeSelf();
    }
    /// <summary>
    /// decreases shootInterval by a percent from originalInterval
    /// </summary>
    protected override void Effect()
    {
       for (int i = 0; i < shipcontroll.projectileInfos.Length; i++)
        {
            shipcontroll.projectileInfos[i].shotInterval -= shipcontroll.projectileInfos[i].originalInterval * increasementPercent;
            shipcontroll.UpdateShootInterval();
        }
    }
}
