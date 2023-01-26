using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// when applied, increases all player projectiles damage by plus "damage" value
/// </summary>
public class DamageIncrease : UpgradeBtnScript
{
    [SerializeField]
    int minval, maxval;
    int damage;
    private void Start()
    {
        //initialize upgrade after being constructed;
        damage = Random.Range(minval, maxval);
        gradeName = "Upgrade blaster canon condensators (+" + damage + " damage)";
        InitializeSelf();
    }
    protected override void Effect()
    {
       for (int i = 0; i < shipcontroll.projectileInfos.Length; i++)
        {
            shipcontroll.projectileInfos[i].damage += damage;
        }
    }
}
