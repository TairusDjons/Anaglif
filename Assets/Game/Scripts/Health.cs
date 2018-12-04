using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthEvent();
public class Health : MonoBehaviour
{
    [SerializeField]
    int startHealth;
    int currentHealth;

    public event HealthEvent OnHealthChanged;
    public event HealthEvent OnDeath;

    public int StartHealth
    {
        get { return startHealth; }
    }


    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        private set
        {
            currentHealth = value > currentHealth ? 0 : value;
            OnHealthChanged();
            if (currentHealth == 0)
                OnDeath();
        }
    }
    private void Start()
    {
        currentHealth = startHealth;
    }
    
    public void TakeDamage(int value)
    {
        CurrentHealth -= value; 
    }
}
