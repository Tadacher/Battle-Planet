using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
/// <summary>
/// contains onwer unit hitpoint system 
/// </summary>
public class HitPointComponent : MonoBehaviour
{   
    public short hitpoints, hpRegen, shield, shieldregen;
    public short maxHp, maxShield;
    protected short originalMaxHp, originalMaxShield;
    [SerializeField]
    Sprite ownsprite;
    [SerializeField]
    GameObject particleSystemPrefab;
    [SerializeField]
    protected float timeToRegen, regenInterval;
    [SerializeField]
    protected EnemyBehaviour enemyBehaviour;
    [SerializeField]
    /// <summary>
    /// no calls for OnDamageRecieved if false
    /// </summary>
    protected bool sendDamageInfo;
    [SerializeField]
    protected bool spawnParticles, isNotCounted;
    ScoreControll scoreControll;
    SfxController sfx;

    [Inject]
    void Construct(SfxController _sfx, ScoreControll _scoreControll)
    {
        sfx = _sfx;
        scoreControll = _scoreControll;
    }
    private void Start()
    {
        maxHp = hitpoints;
        maxShield = shield;
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

        if (hitpoints <= 0)
        {
            if(!isNotCounted) PlusScore();
            Die();
        }
        if(sendDamageInfo) enemyBehaviour.OnDamageRecieved();
    }

    protected virtual void Regen()
    {
        if (hitpoints + hpRegen < maxHp) hitpoints += hpRegen;
        else hitpoints = maxHp;

        if (shield + shieldregen < maxShield) shield += shieldregen;
        else shield = maxShield;
    }

    public void RefreshRegenTime()
    {
        timeToRegen = regenInterval;
    }

    public void Die()
    {
        sfx.PlayEnemyDeathSound();
        if (spawnParticles)
        {
            GameObject partSystem = Instantiate(particleSystemPrefab, transform.position, transform.rotation, null);
            partSystem.GetComponent<SpriteRenderer>().sprite = ownsprite;
        }
        Destroy(gameObject);
    }
    public void PlusScore()
    {
        scoreControll.PlusFrag();
    }
}
