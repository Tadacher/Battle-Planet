using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Legaceer of HitPointComponent for player ship
/// </summary>
public class ShipHitpointComponent : HitPointComponent
{
    // to inject
    ShipBehaviour _shipcontroll;
    UiService _uiService;

    public void Construct(ShipBehaviour shipBehaviour, UiService uiController)
    {
        _shipcontroll = shipBehaviour;
        _uiService = uiController;
        maxHp = hitpoints;
        originalMaxHp = maxHp;
        originalMaxShield = maxShield;
        maxShield = shield;
        _uiService.DrawHP(hitpoints, maxHp);
        _uiService.DrawShield(shield, maxShield);
    }
    public override void RecieveDamage(int damage)
    {
        if (!_shipcontroll.isCrushing)
        {
            if (shield == 0)
            {
                hitpoints -= (short)damage;
                if (hitpoints < 0) hitpoints = 0;
                _uiService.DrawHP(hitpoints, maxHp);
            }
            else if (shield - (short)damage >= 0)
            {
                shield -= (short)damage;
                _uiService.DrawShield(shield, maxShield);
            }
            else if (shield - (short)damage < 0)
            {
                shield = 0;
                _uiService.DrawShield(shield, maxShield);
            }
           _uiService.DrawHP(hitpoints, maxHp);
        }
    }
    protected override void Regen()
    {
        if (hitpoints + hpRegen < maxHp) hitpoints += hpRegen;
        else hitpoints = maxHp;
        _uiService.DrawHP(hitpoints, maxHp);

        if (shield + shieldregen < maxShield) shield += shieldregen;
        else shield = maxShield;
        _uiService.DrawShield(shield, maxShield);
    }
    public void ResetHP()
    {
        hitpoints = maxHp;
        _uiService.DrawHP(hitpoints, maxHp);
    }


}
