using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    CharacterStats myStats;
    //float attackCooldown;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        //attackCooldown = myStats.attackCooldown.GetBaseValue();
        attackCooldown -= Time.deltaTime;

    }

    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0)
        {
            targetStats.TakeDamage(myStats.power.GetBaseValue());
            //attackCooldown = myStats.attackCooldown.GetBaseValue();
            attackCooldown = 3f / attackSpeed;
        }

    }
}
