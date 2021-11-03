using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 2;
    public int Health { get; private set; }

    public bool IsAlive { get => Health > 0; }

    public UnityEvent Die; 

    private void Start()
    {
        Health = maxHealth;

        if (Die == null)
            Die = new UnityEvent();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (!IsAlive)
        {
            Health = 0;
            Die.Invoke();
        }
    }
}
