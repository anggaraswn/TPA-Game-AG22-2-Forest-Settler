using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    private CapsuleCollider capsuleCollider;
    public Interactable focus;
    public HealthBar healthBar;
    public ExpBar expBar;
    public AddSkillPoints addSkillPoints;

    [SerializeField] float speed = 6f;
    [SerializeField] float maxRotateSpeed = 0.1f;
    [SerializeField] Transform camera;


    private float currentVelocity;
    private float gravity = 9.8f;
    public int maxHealth = 100;
    public int currHealth;
    private int maxExp = 250;
    private int currExp;
    private int currLevel = 1;
    private bool canAttack = false;
    public bool haveWeapon = false;
    public int damage = 30;
    public int attackDuration = 10;
    public int currAttackDuration = 0;
    public int AGI = 1;
    public int STR = 1;
    public int POW = 1;
    public int statusPoints = 0;
    List<GameObject> enemyList = new List<GameObject>();

    bool holdingWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currExp = maxExp;
        expBar.SetMaxExp(maxExp);
        expBar.SetCurrLevel(currLevel);
        addSkillPoints = GameObject.Find("AddSkillPoints").GetComponent<AddSkillPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth <= 0)
        {
            SceneManager.LoadScene("DeathMenu");
        }
        else
        {
            Movement();
            Animation();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(currAttackDuration <= 0)
                {
                    if(enemyList.Count > 0)
                    {
                        foreach(GameObject gameObject in enemyList)
                        {
                            gameObject.GetComponentInChildren<Enemy>().TakeDamage(damage);
                        }
                    }
                }
                currAttackDuration = attackDuration;
            }
            if(currAttackDuration > 0)
            {
                currAttackDuration--;
            }
        }

        if(statusPoints > 0)
        {
            addSkillPoints.Show();
        }
        else
        {
            addSkillPoints.Hide();
        }
    }

    private void Die()
    {

    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveDirection = new Vector3();
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, maxRotateSpeed);

            moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            moveDirection = moveDirection.normalized;

            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        moveDirection.y += (gravity * -1);
        moveDirection.x *= speed;
        moveDirection.z *= speed;
        controller.Move(moveDirection * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15f;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = 6f;
        }
    }

    private void Animation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;


        if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            //walk
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsSprinting", false);

            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", Mathf.Abs(direction.z));

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("IsRolling", true);
                animator.SetBool("IsWalking", false);
            }
            else
            {
                animator.SetBool("IsRolling", false);
            }
        }
        else if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            //run
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsSprinting", true);
            animator.SetBool("IsWalking", false);

            animator.SetFloat("DirectionX", 0f);
            animator.SetFloat("DirectionY", 2);

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("IsRolling", true);
                animator.SetBool("IsSprinting", false);
            }
            else
            {
                animator.SetBool("IsRolling", false);
            }
        }
        else if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && direction.z == 0)
        {
            //strafe
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsSprinting", false);

            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", 0f);

        }else if (Input.GetMouseButton(0))
        {
            //if () ;
        }
        else
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsSprinting", false);


            animator.SetFloat("DirectionX", 0f);
            animator.SetFloat("DirectionY", 0f);

        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null) focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);

    }

    private void RemoveFocus()
    {
        if(focus != null) focus.OnDefocused();
        focus = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            enemyList.Remove(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }

    public void AddExp(int exp)
    {
        currExp += exp;
        expBar.SetExp(currExp);

        if(currExp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currExp = 0;
        expBar.SetExp(currExp);
        currLevel += 1;
        expBar.SetCurrLevel(currLevel);
        AddMaxExp();

        currHealth = maxHealth;
        healthBar.SetHealth(currHealth);

    }

    public void AddMaxExp()
    {
        maxExp += 100 * currLevel;
    }

    public void ShowAddSkillPoints()
    {
        addSkillPoints.Show();
    }

    public void HideAddSkillPoints()
    {
        addSkillPoints.Hide();
    }

}
