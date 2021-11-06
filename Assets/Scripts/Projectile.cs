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
    }

    public static void Create(GameObject prefab, Vector2 spawnPosition, Vector2 direction)
    {
        Projectile instance = Instantiate(prefab, (Vector3)spawnPosition, Quaternion.Euler(direction.x, direction.y, 0.0f)).GetComponent<Projectile>();
        instance._direction = direction;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = speed * _direction;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime >= maxLifeTime)
        {
            Explode();
        }
    }

    public void DamageTile(HealthManager tileHealth)
    {
        tileHealth.TakeDamage(terrainDamage);
        Explode();
    }

    public void DamageEnemy(HealthManager enemyHealth)
    {
        enemyHealth.TakeDamage(enemyDamage);
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
