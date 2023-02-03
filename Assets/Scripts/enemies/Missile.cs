using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Missile : Projectile
{   [SerializeField]
    float turnSpeed, speed, lifetime;
    AIDestinationSetter targetSetter;
    
    private void Start()
    {
        targetSetter = GetComponent<AIDestinationSetter>();
        SetTarget();
    }
    private void Update()
    {
        transform.Translate(Vector3.up * speed*Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }
    void SetTarget()
    {
        targetSetter.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}
