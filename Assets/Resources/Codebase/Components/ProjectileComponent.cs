using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField] protected float _timer;
    [SerializeField] protected string _targetTag;
    
    public int _damage;

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == _targetTag)
        {
            other.GetComponent<HitPointComponent>().RecieveDamage(_damage);
            Destroy(gameObject);
        }
    }

}
