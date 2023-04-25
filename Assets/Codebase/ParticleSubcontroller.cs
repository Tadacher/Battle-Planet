using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSubcontroller : MonoBehaviour
{
    [SerializeField]
    float fadeSpeed, maxspeed;
    [SerializeField]
    GameObject[] _particleSystems;
    [SerializeField]
    GameObject blood;

    float rotationDirection;
    Vector3 direction;
    GameObject particleSystemInstance;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        particleSystemInstance = Instantiate(_particleSystems[Random.Range(0, _particleSystems.Length)], transform);
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = new Vector3(Random.Range(0.1f, maxspeed), Random.Range(0.1f, maxspeed), 0f);
    }

    private void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime*fadeSpeed);
        if (spriteRenderer.color.a <= 0.1) Destroy(gameObject);
        transform.Translate(direction*Time.deltaTime);
    }
}
