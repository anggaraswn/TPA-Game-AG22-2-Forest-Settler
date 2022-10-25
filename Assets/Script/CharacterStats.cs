using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat agility;
    public Stat strength;
    public Stat power;
    public Stat attackSpeed;
    public Stat attackCooldown;


    void Awake()
    {
        currentHealth = maxHealth;    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(power.GetBaseValue());
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if(currentHealth <= 0)
        {
            Die();        
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }

}
