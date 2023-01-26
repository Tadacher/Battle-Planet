using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //projectiles logic and stuff
    [SerializeField]
    protected float timer;
    [SerializeField]
    protected string targetTag;
    
    public int damage;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == targetTag)
        {
            other.GetComponent<HitPointComponent>().RecieveDamage(damage);
            Destroy(gameObject);
        }
    }

}
