using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent (typeof(CharacterStats))]
public class Enemy : Interactable
{
    //PlayerManager playerManager;
    //CharacterStats myStats;

    
    public float lookRadius = 15f;
    public float speed = 5.0f;
    public Transform target;
    public NavMeshAgent agent;
    public Animator animator;
    public EnemyHealthBar healthBar;
    public int maxHealth = 1000;
    public int currHealth { get; private set; }
    public int EXP;
    public int damage = 20;
    public float attackCD = 30f;
    public float currAttackCD = 0f;
    public bool isAlive = true;
    public bool isAttack = false;
    public bool attack01, attack02, attack03;
    public float nextHitTime = -1f;
    public Player player;
    public Outline outline;
    public string name;
    public Transform lookAt;
    public EnemyCamp camp;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        target = GameObject.Find("Character").transform;
        currHealth = maxHealth;
        attack01 = true;
        attack02 = attack03 = false;
        player = target.gameObject.GetComponent<Player>();
        outline = GetComponent<Outline>();
        healthBar.SetName(name);
    }

    void Update()
    {
        if(currHealth <= 0)
        {
            if (isAlive)
            {
                isAlive = false;
                Die();
            }
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                healthBar.Show();
                outline.OutlineWidth = 3f;
                agent.SetDestination(target.position);
                Animation(1);
                isAttack = false;

                if (distance <= agent.stoppingDistance)
                {
                    Animation(2);
                    FaceTarget();
                    player.TakeDamage(damage);
                    isAttack = true;
                }
            }
            else
            {
                Animation(0);
                agent.ResetPath();
                isAttack = false;
                healthBar.Hide();
                outline.OutlineWidth = 0;
            }

            //if(currAttackCD >= 0)
            //{
            //    currAttackCD--;
            //}
            //else
            //{
            //    currAttackCD = attackCD;
            //}

            if (isAttack)
            {
                if(Time.time >= nextHitTime)
                {
                    target.gameObject.GetComponent<Player>().TakeDamage(damage);
                    nextHitTime = Time.time + attackCD;
                }
            }
        }

       

        //if (distance <= 5)
        //{
        //    if (isAttacking)
        //    {
        //        StartCoroutine(Attack());
        //    }
        //}
        //else
        //{
        //    animator.SetBool("IsAttack", false);
        //}

    }
    //public override void Interact()
    //{
    //    base.Interact();
    //    CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
    //    if(playerCombat != null)
    //    {
    //        playerCombat.Attack(myStats);
    //    }
    //}

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

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        Debug.Log(name + " get damage " + damage);
    }

    public void Die()
    {
        player.AddExp(EXP);
        agent.ResetPath();
        Animation(-1);
        Destroy(gameObject);
    }

    void Animation(int targetAnimation)
    {
        if (targetAnimation == 0 )
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsAttacking", false);
        }else if(targetAnimation == 1)
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsAttacking", false);
        }else if(targetAnimation == 2)
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsDeath", true);
            //animator.SetFloat("AttackType", -1f);
        }
    }

    public void SetCamp(EnemyCamp camp)
    {
        this.camp = camp;
    }
}
