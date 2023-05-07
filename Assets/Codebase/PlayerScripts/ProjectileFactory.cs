using UnityEngine;
public class ProjectileFactory
{
    public void SpawnBullet(ShipBehaviour.ProjectileInfo projectileInfo, Transform projectileSpawner)
    {
        GameObject bullet = GameObject.Instantiate(projectileInfo.projectilePrefab, projectileSpawner.position, projectileSpawner.rotation, null);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 10, ForceMode2D.Impulse);
        
        Projectile projScript = bullet.GetComponent<Projectile>();
        projScript.damage = projectileInfo.damage;
    }
}