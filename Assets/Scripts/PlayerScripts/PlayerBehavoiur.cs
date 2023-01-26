using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Legaceer of HitPointComponent for player ship
/// </summary>
public class PlayerBehavoiur : HitPointComponent
{
    // to inject
    Shipcontroll shipcontroll;
    UiController uiController;
    //
    [Inject]
    void Construct(UiController _uiController)
    {
        uiController = _uiController;
    }
    
    private void Start()
    {
        //initialize
        shipcontroll = gameObject.GetComponent<Shipcontroll>();
        maxHp = hitpoints;
        originalMaxHp = maxHp;
        originalMaxShield = maxShield;
        maxShield = shield;
        uiController.DrawHP(hitpoints, maxHp);
        uiController.DrawShield(shield, maxShield);
    }
    public override void RecieveDamage(int damage)
    {
        if (!shipcontroll.isCrushing)
        {
            if (shield == 0)
            {
                hitpoints -= (short)damage;
                if (hitpoints < 0) hitpoints = 0;
                uiController.DrawHP(hitpoints, maxHp);
            }
            else if (shield - (short)damage >= 0)
            {
                shield -= (short)damage;
                uiController.DrawShield(shield, maxShield);
            }
            else if (shield - (short)damage < 0)
            {
                shield = 0;
                uiController.DrawShield(shield, maxShield);
            }
            shipcontroll.IfDamageRecieved(hitpoints);
        }
    }
    protected override void Regen()
    {
        if (hitpoints + hpRegen < maxHp) hitpoints += hpRegen;
        else hitpoints = maxHp;
        uiController.DrawHP(hitpoints, maxHp);

        if (shield + shieldregen < maxShield) shield += shieldregen;
        else shield = maxShield;
        uiController.DrawShield(shield, maxShield);
    }
    public void ResetHP()
    {
        hitpoints = maxHp;
        uiController.DrawHP(hitpoints, maxHp);
    }


}
