using UnityEngine;
using Zenject;

public class PlanetHitpoints : HitPointComponent
{
    private short _originalHp;
    private UiService _uiService;
    
    [Inject]
    void Construct(UiService uiservice, ScoreService _scoreControll)
    {
        _uiService = uiservice;
        base._scoreControll = _scoreControll;
    }
    private void Start()
    {
        _maxHp = _hitpoints;
        _originalHp = _maxHp;
        _maxShield = shield;
        _uiService.DrawPlanetHp(_hitpoints, _maxHp);
    }
    private void Update()
    {
        timeToRegen -= Time.deltaTime;
        if (timeToRegen <= 0)
        {
            RefreshRegenTime();
            Regen();
        }
    }

    public override void RecieveDamage(int damage)
    {
        if (shield == 0) _hitpoints -= (short)damage;
        else if (shield - (short)damage >= 0) shield -= (short)damage;
        else if (shield - (short)damage < 0) shield = 0;

        if (_hitpoints <= 0) GameOver();

        _uiService.DrawPlanetHp(_hitpoints, _maxHp);
    }
    protected override void Regen()
    {
        base.Regen();
        _uiService.DrawPlanetHp(_hitpoints, _maxHp);
    }
    public void GameOver()
    {

    }
}
