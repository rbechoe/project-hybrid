using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatWhiteAI : MonoBehaviour
{
    public bool attack;
    public float attackCd;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
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
    }
}
