using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform target;
    public NavMeshAgent agent;
    public Animator animator;
    private CharacterStats stats;
    private bool isAttacking = false;
    public int damage = 10;
    public int currHealth;
    public int maxHealth = 100;
    public int attackDelay;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        stats = FindObjectOfType<CharacterStats>();
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        if(distance <= 5)
        {
            if (isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            animator.SetBool("IsAttack", false);
        }

        if(currHealth <= 0)
        {
            //Up
        }
    }

    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            yield return new WaitForSeconds(3f);
            stats.TakeDamage(stats.power.GetBaseValue());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
