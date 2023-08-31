using UnityEngine;
public class ProjectileFactory
{
    public void SpawnBullet(ProjectileData projectileInfo, Transform projectileSpawner)
    {
        GameObject bullet = GameObject.Instantiate(projectileInfo.projectilePrefab, projectileSpawner.position, projectileSpawner.rotation, null);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 10, ForceMode2D.Impulse);
        
        ProjectileComponent projScript = bullet.GetComponent<ProjectileComponent>();
        projScript._damage = projectileInfo.damage;
    }
}