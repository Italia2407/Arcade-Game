using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    public float speed = 1.0f;

    private Vector2 _direction;

    public int terrainDamage = 1;
    public int enemyDamage = 1;

    public float maxLifeTime = 10.0f;
    private float _lifeTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        _rigidbody.velocity = speed * _direction;

        _lifeTime += Time.deltaTime;
        if (_lifeTime >= maxLifeTime)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag == "Terrain")
        {
            collidedObject.GetComponent<HealthManager>().TakeDamage(terrainDamage);
            Explode();
        }
        else if (collidedObject.tag == "Enemies")
        {
            collidedObject.GetComponent<HealthManager>().TakeDamage(enemyDamage);
            Explode();
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
