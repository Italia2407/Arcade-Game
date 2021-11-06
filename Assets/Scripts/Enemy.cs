using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private HealthManager _healthManager;

    public int terrainDamage = 1;
    public int playerDamage = 1;

    private Vector2 _velocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _healthManager = GetComponent<HealthManager>();

        _healthManager.Die.AddListener(OnDeath);
    }

    private void FixedUpdate()
    {
        _velocity = _rigidbody.velocity;

        Debug.Log(transform.position.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag == "Projectile")
        {
            Debug.Log("Shot");
            collidedObject.GetComponent<Projectile>().DamageEnemy(_healthManager);

            _rigidbody.velocity = _velocity;
        }
        else if (collidedObject.tag == "Player")
        {
            _rigidbody.velocity = _velocity;
        }
    }

    public void DamageTile(HealthManager tileHealth)
    {
        tileHealth.TakeDamage(terrainDamage);
        OnDeath();
    }

    public void DamagePlayer(HealthManager playerHealth)
    {
        playerHealth.TakeDamage(playerDamage);
        OnDeath();
    }

    private void OnDeath()
    {
        _healthManager.Die.RemoveAllListeners();

        Destroy(gameObject);
    }
}
