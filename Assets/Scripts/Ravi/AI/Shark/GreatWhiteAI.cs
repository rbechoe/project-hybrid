using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreatWhiteAI : MonoBehaviour, IDamagable
{
    public int hp = 100;
    public float attackCd = 1;
    public float graceTime = 20;
    public float detectRange = 20;

    private Animator anim;
    private GameObject player;
    private SharkCart cart;
    public GameObject cartObj;

    public Action sharkIdle;
    public Action[] sharkAttacks;
    private FiniteStateMachine FSM;
    private FiniteStateMachine.State idleState;
    private FiniteStateMachine.State performAction;
    private readonly Queue<Action> actions = new Queue<Action>();

    public AudioClip attackSFX, dieSFX;
    private AudioSystem audioSystem;
    private bool attacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        cart = cartObj.GetComponent<SharkCart>();

        FSM = new FiniteStateMachine();
        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();

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
            Action action = actions.Peek();
            bool success = action.PerformAction();

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

    private void Update()
    {
        FSM.Update(gameObject);

        if (!attacking)
        {
            if (attackCd >= 0) attackCd -= Time.deltaTime;

            if (Vector3.Distance(transform.position, player.transform.position) < detectRange && attackCd <= 0)
            {
                audioSystem.ShootSFX(attackSFX, transform.position);
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
            EventSystem<int>.InvokeEvent(EventType.SCORE_UP, 500);
            audioSystem.ShootSFX(dieSFX, transform.position);
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
