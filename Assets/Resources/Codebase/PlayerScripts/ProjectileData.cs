using UnityEngine;

public struct ProjectileData
{
    public int damage;
    public int OriginalDamage { private set; get; }
    public GameObject projectilePrefab;
    public int shotSoundId;
    public float shotInterval;
    public float OriginalInterval { private set; get; }
    public ProjectileData(int dmg, GameObject projPrefab, int shotSnd, float shotIntervl)
    {
        damage = dmg;
        OriginalDamage = damage;
        projectilePrefab = projPrefab;
        shotSoundId = shotSnd;
        shotInterval = shotIntervl;
        OriginalInterval = shotInterval;
    }
}