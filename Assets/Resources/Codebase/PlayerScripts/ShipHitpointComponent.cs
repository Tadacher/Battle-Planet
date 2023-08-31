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
        _maxHp = _hitpoints;
        originalMaxHp = _maxHp;
        originalMaxShield = _maxShield;
        _maxShield = shield;
        _uiService.DrawHitpoints(_hitpoints, _maxHp);
        _uiService.DrawShield(shield, _maxShield);
    }
    public override void RecieveDamage(int damage)
    {
        if (!_shipcontroll.isCrushing)
        {
            if (shield == 0)
            {
                _hitpoints -= (short)damage;
                if (_hitpoints < 0) _hitpoints = 0;
                _uiService.DrawHitpoints(_hitpoints, _maxHp);
            }
            else if (shield - (short)damage >= 0)
            {
                shield -= (short)damage;
                _uiService.DrawShield(shield, _maxShield);
            }
            else if (shield - (short)damage < 0)
            {
                shield = 0;
                _uiService.DrawShield(shield, _maxShield);
            }
           _uiService.DrawHitpoints(_hitpoints, _maxHp);
        }
    }
    protected override void Regen()
    {
        if (_hitpoints + hpRegen < _maxHp) _hitpoints += hpRegen;
        else _hitpoints = _maxHp;
        _uiService.DrawHitpoints(_hitpoints, _maxHp);

        if (shield + shieldregen < _maxShield) shield += shieldregen;
        else shield = _maxShield;
        _uiService.DrawShield(shield, _maxShield);
    }
    public void ResetHP()
    {
        _hitpoints = _maxHp;
        _uiService.DrawHitpoints(_hitpoints, _maxHp);
    }


}
