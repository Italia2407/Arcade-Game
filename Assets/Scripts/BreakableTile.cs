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

    public static void Create(GameObject prefab, Vector2 spawnPosition, Sprite sprite, Transform parent)
    {
        SpriteRenderer instanceSprite = Instantiate(prefab, (Vector3)spawnPosition, Quaternion.identity, parent).GetComponent<SpriteRenderer>();
        instanceSprite.sprite = sprite;
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
            collidedObject.GetComponent<Projectile>().DamageTile(_healthManager);
        }
    }

    private void OnBreak()
    {
        Destroy(gameObject);
    }
}
