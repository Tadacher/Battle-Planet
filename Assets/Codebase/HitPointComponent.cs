using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
/// <summary>
/// contains onwer unit hitpoint system 
/// </summary>
public class HitPointComponent : MonoBehaviour, IDamageReciever
{   
    public short hitpoints, hpRegen, maxHp, shield, shieldregen, maxShield;
    protected short originalMaxHp, originalMaxShield, originalHpRegen, originalShieldRegen;
    [SerializeField] protected float timeToRegen, regenInterval;
    
    [SerializeField] GameObject particleSystemPrefab;
    [SerializeField]
    /// <summary>
    /// no calls for OnDamageRecieved if false
    /// </summary>
    protected bool sendDamageInfo;
    [SerializeField] protected bool spawnParticles;
    protected ScoreControll _scoreControll;
    SfxService _sfxService;

    [Inject]
    void Construct(SfxService sfx, ScoreControll scoreControll)
    {
        _sfxService = sfx;
        _scoreControll = scoreControll;
        InitializeSelf();
    }
    private void InitializeSelf()
    {
        maxHp = hitpoints;
        originalHpRegen = hpRegen;
        originalMaxHp = maxHp;

        maxShield = shield;
        originalShieldRegen = shieldregen;
        originalMaxShield = maxShield;
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

        if (shield == 0) hitpoints -= (short)damage;
        else if (shield - (short)damage >= 0) shield -= (short)damage;
        else if (shield - (short)damage < 0) shield = 0;

        if (hitpoints <= 0) Die();
    }

    protected virtual void Regen()
    {
        if (hitpoints + hpRegen < maxHp) hitpoints += hpRegen;
        else hitpoints = maxHp;

        if (shield + shieldregen < maxShield) shield += shieldregen;
        else shield = maxShield;
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

    public void PlusScore() => _scoreControll.PlusScore(1);
}
