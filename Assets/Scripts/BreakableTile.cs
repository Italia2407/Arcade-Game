using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class BreakableTile : MonoBehaviour
{
    private HealthManager _healthManager;

    private void Start()
    {
        _healthManager = GetComponent<HealthManager>();

        _healthManager.Die.AddListener(OnBreak);
    }

    private void OnBreak()
    {
        Destroy(gameObject);
    }
}
