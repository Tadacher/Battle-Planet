using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AssaulFrigate : EnemyBehaviour
{
    [SerializeField]
    float alphaDecrease;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    HitPointComponent hpComponent;

    [SerializeField]
    int damage;
    [Inject]
    void Construct(PlanetHitpoints _planetHitpoints, ScoreControll _scoreControll)
    {
        player = _planetHitpoints.transform;
    }

    private void Start()
    {
        initialpos = transform.position;
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        hpComponent = GetComponent<HitPointComponent>();
    }
    void Update()
    {
        //force field effect activation
        if (spriteRenderer.color.a >= 0)
        {
            spriteRenderer.color -= new Color(0f, 0f, 0f, alphaDecrease * Time.deltaTime);
        } 

        transform.Translate(transform.right.normalized * multArgX * Time.deltaTime, null);
        multArgX -= multArgXDecreaser * Time.deltaTime;
    }

    public override void OnDamageRecieved()
    {
        hpComponent.RefreshRegenTime();
        if (hpComponent.shield > 0) spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        else spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag is "Player")
        {
            collision.gameObject.GetComponent<HitPointComponent>().RecieveDamage(damage * 2);
            hpComponent.Die();
            scoreControll.PlusFrag();

        }
        else if(collision.transform.tag is "Planet")
        {
            collision.gameObject.GetComponent<HitPointComponent>().RecieveDamage(damage);
            hpComponent.Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag is "Player")
        {
            collision.gameObject.GetComponent<HitPointComponent>().RecieveDamage(damage * 2);
            hpComponent.Die();
            scoreControll.PlusFrag();

        }
        else if (collision.transform.tag is "Planet")
        {
            collision.gameObject.GetComponent<HitPointComponent>().RecieveDamage(damage);
            hpComponent.Die();
        }
    }


}
