using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlanetHitpoints : HitPointComponent
{
    short originalHp;
    UiController uiController;
    ScoreControll scoreControll;
    [Inject]
    void Construct(UiController _uicontroller, ScoreControll _scoreControll)
    {
        uiController = _uicontroller;
        scoreControll = _scoreControll;
    }
    private void Start()
    {
        maxHp = hitpoints;
        originalHp = maxHp;
        maxShield = shield;
        uiController.DrawPlanetHp(hitpoints, maxHp);
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
        if (shield == 0) hitpoints -= (short)damage;
        else if (shield - (short)damage >= 0) shield -= (short)damage;
        else if (shield - (short)damage < 0) shield = 0;

        if (hitpoints <= 0) GameOver();
        if (sendDamageInfo) enemyBehaviour.OnDamageRecieved();

        uiController.DrawPlanetHp(hitpoints, maxHp);
    }
    protected override void Regen()
    {
        base.Regen();
        uiController.DrawPlanetHp(hitpoints, maxHp);
    }
    public void GameOver()
    {
        scoreControll.GameOver();
    }
}
