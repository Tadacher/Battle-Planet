using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Zenject;
using Infrastructure;

public class MissileCruiser : EnemyBehaviour
{
    AIDestinationSetter targetSetter;
    SoundEffectsService sfx;
    LocationInstaller locationInstaller;
    [SerializeField]
    bool isChangingPosition;
    [SerializeField]
    GameObject projectile, target;

    [SerializeField]
    Transform spawner;

    [SerializeField]
    float timeTofire, minDistanceToPlanet, maxDistanceToPlanet, minDistToNexWaypoint;
    float curTimeTofire;

    [Inject]
    void Construct(SoundEffectsService _sfx, LocationInstaller loc)
    {
        sfx = _sfx;
        locationInstaller = loc;
    }
    private void Start()
    {
        targetSetter = GetComponent<AIDestinationSetter>();
        initialpos = transform.position;
        target.transform.parent = null;
        SetNewDestination();


    }

    private void Update()
    {
        curTimeTofire -= Time.deltaTime;
        if (curTimeTofire <= 0)
        {
            curTimeTofire = timeTofire;
            Fire();
        }

        CalculateLookDirection();
        CalculateMultargs();

      if (Mathf.Abs((transform.position - target.transform.position).magnitude) < 0.5f) targetSetter.target = null;
    }

    protected override void Fire()
    {
        GameObject missile = locationInstaller.CreateEnemy(projectile, spawner);
        sfx.PlayMissileLaunchSound();
    }

    void SetNewDestination()
    {
        Debug.Log(Mathf.Abs((transform.position - target.transform.position).magnitude));

        //if distance between self and target position is enough small
        if (Mathf.Abs((transform.position - target.transform.position).magnitude) < 1.2f)
        {
            //randomly sets x and y ccord to be hegher or lover then 0,generates x and y in preset range
            target.transform.position = new Vector3(
                ((Random.Range((int)0, (int)2)) == 0 ?
                    Random.Range(minDistanceToPlanet, maxDistanceToPlanet) :
                    -1f * Random.Range(minDistanceToPlanet, maxDistanceToPlanet)),
                 (Random.Range((int)0, (int)2)) == 0 ?
                    Random.Range(minDistanceToPlanet, maxDistanceToPlanet) :
                   -1f * Random.Range(minDistanceToPlanet, maxDistanceToPlanet),
                 0f);
            if (Mathf.Abs((transform.position - target.transform.position).magnitude) < minDistToNexWaypoint) SetNewDestination(); 

            targetSetter.target = target.transform;
        }
    }
    public override void OnDamageRecieved()
    {
        SetNewDestination();
    }


}
