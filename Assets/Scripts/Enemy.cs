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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag == "Terrain")
        {
            collidedObject.GetComponent<HealthManager>().TakeDamage(terrainDamage);
        }
        else if (collision.gameObject.tag == "Player")
        {
            collidedObject.GetComponent<HealthManager>().TakeDamage(playerDamage);
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
