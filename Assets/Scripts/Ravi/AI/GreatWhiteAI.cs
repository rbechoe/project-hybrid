using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatWhiteAI : MonoBehaviour, IDamagable
{
    bool attacking = false;

    public int hp = 100;
    public bool attack;
    public float attackCd;
    public float graceTime = 20; // attack chance when at zero
    public float detectRange = 20;
    public float attackRange = 5;
    public float followSpeed = 5f;

    float cabinRange, attackPosRange;

    Animator anim;
    GameObject player, attackPos;
    SharkCart cart;
    public GameObject cartObj;

    FiniteStateMachine FSM;
    FiniteStateMachine.State patrolState;
    FiniteStateMachine.State attackState;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackPos = GameObject.FindGameObjectWithTag("PlayerAttack");
        cart = cartObj.GetComponent<SharkCart>();

        FSM = new FiniteStateMachine();
    }

    void PatrolState()
    {

    }

    void AttackState()
    {

    }

    void Update()
    {
        attackPosRange = Vector3.Distance(transform.position, attackPos.transform.position);
        cabinRange = Vector3.Distance(transform.position, player.transform.position);

        if (attack)
        {
            attack = false;
            if (attackCd <= 0)
            {
                // do attack
                anim.SetTrigger("Attack");
                attackCd = 1;
            }
        }

        if (attackCd >= 0) attackCd -= Time.deltaTime;

        // shark goes to attack position and then looks at cabin when doing attack
        // becomes agressive when not in attack behaviour but when getting attacked
        // can do attack when attackpos distance is closer than cabin pos and when grace is <= 0
        if (Vector3.Distance(transform.position, player.transform.position) < detectRange)
        {

        }

        // follow cart
        if (!attacking)
        {
            cart.moving = true;
            cart.moveSpeed = followSpeed;

            Vector3 targetDir = cart.transform.position - transform.position;
            float step = followSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            // Move forward
            transform.position += transform.forward * followSpeed * Time.deltaTime;
            if (transform.position.y <= 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}