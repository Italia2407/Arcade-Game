using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class BreakableTile : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _boxCollider;
    private HealthManager _healthManager;

    private void Start()
    {
        _healthManager = GetComponent<HealthManager>();

        _healthManager.Die.AddListener(OnBreak);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag == "Enemies")
        {
            collidedObject.GetComponent<Enemy>().DamageTile(_healthManager);
        }
        else if (collidedObject.tag == "Projectile")
        {

        }
    }

    private void OnBreak()
    {
        Destroy(gameObject);
    }
}
