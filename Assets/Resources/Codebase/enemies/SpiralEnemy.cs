using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Infrastructure;

public class SpiralEnemy : EnemyBehaviour
{
    SfxService sfx;
    LocationInstaller locationInstaller;
    [SerializeField]
    Transform turret, target, spawner;
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float timerToFire;

    float currentTimer;
    float projectileSpeed = 4;

    [Inject]
    void Construct(ShipBehaviour shipcontroll, SfxService _sfx, LocationInstaller loc)
    {
        sfx = _sfx;
        target = shipcontroll.gameObject.transform;
        locationInstaller = loc;
    }


    private void Start()
    {
        timePast += Random.Range(0f, 2f);
        initialpos = new Vector3 (0f, 0f, 0f);
    }
    void Update()
    {
        CalculateTime();

        prevPos = transform.position;

        CalculateTimePast();

        float x = CalculateX();
        float y = CalculateY();
        transform.position = new Vector3(x, y, 0);
        
        TurnTurret();    
    }

    private void LateUpdate()
    {     
        curPos = transform.position;
       transform.rotation = CalculateLookDirection();
        CalculateMultargs();

    }
    protected float CalculateX()
    {     
            return Mathf.Cos(timePast) * multArgX + sumArgX + initialpos.x;  
    }

    protected float CalculateY()
    {
            return Mathf.Sin(timePast) * multArgY + sumArgY + initialpos.y;
    }
    protected void TurnTurret()
    {
        Vector2 lookdirection = target.position - turret.position;
        turret.rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);
    }
    protected override void Fire()
    {
        GameObject bullet = locationInstaller.CreateEnemy(projectile, spawner); 
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
        sfx.PlayenemyCanoneerShotSound();
    }
    
    /// <summary>
    /// calculates time till fire and calls Fire()
    /// </summary>
    private void CalculateTime()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            Fire();
            currentTimer = timerToFire;
        }
    }
}
