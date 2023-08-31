using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
/// <summary>
/// contains onwer unit hitpoint system 
/// </summary>
public class HitPointComponent : MonoBehaviour, IDamageReciever
{   
    public short _hitpoints, hpRegen, _maxHp, shield, shieldregen, _maxShield;
    protected short originalMaxHp, originalMaxShield, originalHpRegen, originalShieldRegen;
    [SerializeField] protected float timeToRegen, regenInterval;
    
    [SerializeField] GameObject particleSystemPrefab;
    [SerializeField]
    /// <summary>
    /// no calls for OnDamageRecieved if false
    /// </summary>
    protected bool sendDamageInfo;
    [SerializeField] protected bool spawnParticles;
    protected ScoreService _scoreControll;
    SfxService _sfxService;

    [Inject]
    void Construct(SfxService sfx, ScoreService scoreControll)
    {
        _sfxService = sfx;
        _scoreControll = scoreControll;
        InitializeSelf();
    }
    private void InitializeSelf()
    {
        _maxHp = _hitpoints;
        originalHpRegen = hpRegen;
        originalMaxHp = _maxHp;

        _maxShield = shield;
        originalShieldRegen = shieldregen;
        originalMaxShield = _maxShield;
    }

    private void Update()
    {
        timeToRegen -= Time.deltaTime;
        if(timeToRegen<=0)
        {
            RefreshRegenTime();
            Regen();
        }
    }
    public virtual void RecieveDamage(int damage)
    {

        if (shield == 0) _hitpoints -= (short)damage;
        else if (shield - (short)damage >= 0) shield -= (short)damage;
        else if (shield - (short)damage < 0) shield = 0;

        if (_hitpoints <= 0) Die();
    }

    protected virtual void Regen()
    {
        if (_hitpoints + hpRegen < _maxHp) _hitpoints += hpRegen;
        else _hitpoints = _maxHp;

        if (shield + shieldregen < _maxShield) shield += shieldregen;
        else shield = _maxShield;
    }

    public void RefreshRegenTime() => timeToRegen = regenInterval;

    public void Die()
    {
        PlayDeathSound();
        if (spawnParticles)
        {
            //GameObject partSystem = Instantiate(particleSystemPrefab, transform.position, transform.rotation, null);
            // partSystem.GetComponent<SpriteRenderer>().sprite = ownsprite;
        }
        Destroy(gameObject);
    }

    protected virtual void PlayDeathSound() => _sfxService.PlayEnemyDeathSound();

    public void PlusScore() => _scoreControll.AddScore(1);
}
