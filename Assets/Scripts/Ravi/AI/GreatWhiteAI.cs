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

    Animator anim;
    GameObject player;
    SharkCart cart;
    public GameObject cartObj;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cart = cartObj.GetComponent<SharkCart>();
    }

    void Update()
    {
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

        // shark attack:
        // come from behind player
        // move to forward window
        // attack player from the front side so that the player can damage the shark
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
