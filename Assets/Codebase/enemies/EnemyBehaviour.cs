using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// contains basic data and classes for enemy behaviour logic
/// </summary>
public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Vector2 initialpos;

    public Transform player;
    protected ScoreControll scoreControll;
    //trajectory zone
    [SerializeField]
    protected float 
        sumArgX, sumArgY, 
        multArgX = 1, multArgY = 1,        
        sumArgXDecreaser, sumArgYDecreaser,
        multArgXDecreaser, multArgYDecreaser,
        timePast;

    //transform.rotation zone
    protected Vector3 prevPos, /*position on update*/ curPos;  //position on lateupdate

    void Start()
    {
        initialpos = transform.position;
    }

    protected void CalculateTimePast()
    {
        timePast += Time.deltaTime * speed;
    }

    /// <summary>
    /// returns quternion fo an object turned to the vector of movement
    /// </summary>
    protected Quaternion CalculateLookDirection()
    {
        if (curPos - prevPos != Vector3.zero)
        {
            Vector2 lookdirection = curPos - prevPos;
            return Quaternion.LookRotation(Vector3.forward, lookdirection);
        }
        else
        {
            return transform.rotation;   
        }
    }

    protected virtual void Fire()
    {
        return;
    }
    protected void CalculateMultargs()
    {
        multArgX -= multArgXDecreaser*Time.deltaTime;
        multArgY -= multArgYDecreaser*Time.deltaTime;
    }

    /// <summary>
    /// caled every time HitPointComponent.RecieveDamage() called 
    /// </summary>
    public virtual void OnDamageRecieved()
    {
        return;
    }   
}
