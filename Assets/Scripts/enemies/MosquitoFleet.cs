using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MosquitoFleet : EnemyBehaviour
{
    int damage = 5;
    HitPointComponent hitPointComponent;
   public  enum TrajType
    {
        straight,
        sin,
        circle,
        ai
    }
    public TrajType trajType = TrajType.straight;
    
    void Start()
    {
        initialpos = transform.position;
        SetInitialLookDirection();
        hitPointComponent = GetComponent<HitPointComponent>();
    }
    void Update()
    {
        CalculateTimePast();
        prevPos = transform.position;
        TranslatePos();
    }

    private void LateUpdate()
    {
        curPos = transform.position;
        transform.GetChild(0).transform.rotation = CalculateLookDirection();
        CalculateMultargs();
        //Debug.Log(CalculateLookDirection().eulerAngles);
    }
    void TranslatePos()
    {
        if (trajType == TrajType.straight) transform.position += transform.up * speed * Time.deltaTime;
        else if (trajType == TrajType.sin)
        {
            transform.position += transform.up * multArgX * Time.deltaTime;
            transform.position += transform.right * Mathf.Sin(timePast) * multArgY * Time.deltaTime;
        }
        else if (trajType == TrajType.circle)
        {
            transform.position = new Vector3(Mathf.Cos(timePast) * multArgX + sumArgX, Mathf.Sin(timePast) * multArgY + sumArgY, 0f);
        }
    }


    void SetRandomTraj()
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                trajType = TrajType.straight;
                break;
            case 1:
                trajType = TrajType.sin;
                break;
            case 2:
                trajType = TrajType.circle;
                break;
            case 3:
                trajType = TrajType.ai;
                break;
        }  
    }

    void SetInitialLookDirection()
    {
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 270);
    }

    public void InitAi ()
    {
        AIDestinationSetter aiDest = GetComponent<AIDestinationSetter>();
        aiDest.enabled = true;
        aiDest.target = player;
        AIPath aIpath = GetComponent<AIPath>();
        aIpath.enabled = true;
        aIpath.maxSpeed = speed;
    }

    public void SetCircleArguments()
    {
        multArgX = 15f;
        multArgY = 15f;
        multArgXDecreaser = 0.3f;
        multArgYDecreaser = 0.3f;
        speed = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag is "Player")
        {
            collision.gameObject.GetComponent<HitPointComponent>().RecieveDamage(damage * 2);
            hitPointComponent.Die();
        }
        else if (collision.transform.tag is "Planet")
        {
            collision.gameObject.GetComponent<HitPointComponent>().RecieveDamage(damage);
            hitPointComponent.Die();
        }
    }
}
