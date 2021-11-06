using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public GameObject projectile;

    public float maxAimAngle = 120.0f;

    private float _maxAngle;
    private float _minAngle;

    private void Start()
    {
        _maxAngle = (maxAimAngle + 90.0f) * Mathf.Deg2Rad;
        _minAngle = (-maxAimAngle + 90.0f) * Mathf.Deg2Rad;
    }

    private void Update()
    {
        Vector3 pointedDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(pointedDirection.y, pointedDirection.x);
        angle = Mathf.Clamp(angle, _minAngle, _maxAngle);

        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile(angle);
        }
    }

    private void ShootProjectile(float directionAngle)
    {
        Vector2 direction = new Vector2(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle));
        Projectile.Create(projectile, transform.position, direction);
    }
}
