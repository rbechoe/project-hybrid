using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreatWhiteAI : MonoBehaviour, IDamagable
{
    public int hp = 100;
    public float attackCd;
    public float graceTime = 20;
    public float detectRange = 20;

    Animator anim;
    GameObject player;
    SharkCart cart;
    public GameObject cartObj;

    public Action sharkIdle;
    public Action[] sharkAttacks;
    FiniteStateMachine FSM;
    FiniteStateMachine.State idleState;
    FiniteStateMachine.State performAction;
    private readonly Queue<Action> actions = new Queue<Action>();

    bool attacking;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cart = cartObj.GetComponent<SharkCart>();

        FSM = new FiniteStateMachine();

        CreateIdleState();
        CreateActionState();
    }

    private void CreateIdleState()
    {
        sharkIdle.GetComponent<SharkPatrol>()?.SetParams(anim, cart, cartObj);
        idleState = (fsm, gameObj) =>
        {
            if (actions.Any())
            {
                fsm.PopState();
                fsm.PushState(performAction);
            }
        };
        FSM.PushState(idleState);
    }

    private void CreateActionState()
    {
        performAction = (fsm, gameObj) =>
        {
            var action = actions.Peek();

            var success = action.PerformAction();

            if (success)
            {
                action.DoReset();
                Transfer(action);

                fsm.PopState();
                fsm.PushState(idleState);
            }
        };

        // used to rotate between attack behaviours if there are many
        foreach (Action action in sharkAttacks)
        {
            action.GetComponent<SharkAttack>()?.SetParams(anim, cart, cartObj, this);
            actions.Enqueue(sharkIdle); // always queue an idle state before an attack state
            actions.Enqueue(action);
        }
    }

    private void Transfer(Action action)
    {
        actions.Dequeue();
        actions.Enqueue(action);
    }

    void Update()
    {
        FSM.Update(gameObject);

        if (!attacking)
        {
            if (attackCd >= 0) attackCd -= Time.deltaTime;

            if (Vector3.Distance(transform.position, player.transform.position) < detectRange && attackCd <= 0)
            {
                attacking = true;
                attackCd = graceTime;
                Transfer(actions.Peek());
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gameObject.GetComponent<EnemyInfo>().dead = true;
            anim.SetTrigger("Dead");
            Destroy(this);
        }
    }

    public void ExitAttack()
    {
        attacking = false;
        Transfer(actions.Peek());
    }
}